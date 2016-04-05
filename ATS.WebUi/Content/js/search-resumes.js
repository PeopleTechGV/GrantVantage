function SearchResume() { }
SearchResume.Isvisisted = false;
SearchResume.DateTimeFOP = [13, 14, 15, 16];
SearchResume.IntFOP = [11, 12];
SearchResume.CountFOP = [11];
SearchResume.DateTimeDaysOP = [15, 16];
SearchResume.NumberOP = [2, 5, 7, 10, 11, 12];
SearchResume.containsOP = [2, 5];
SearchResume.PriorityOperator = [9];
SearchResume.OperatorName = ['Contains Data', 'Does Not Contain Data']

function ApplyFunctions() {
    $('ul.new-criteria.options-dd').each(function (index, ele) {
      
        var $criteria_list = $(ele);
        // console.log($criteria_list);

        for (criteria_key in search_groups) {
            var criteria_group = search_groups[criteria_key];
            // console.log(criteria_group);
            var $new_criteria_item = $criteria_list.find('li.template').clone().removeClass('template');
            $new_criteria_item.find('img').attr('src', criteria_group.icon);
            $new_criteria_item.find('.label').text(criteria_group.display);
            $new_criteria_item.attr('data-criteria-key', criteria_key);
            $criteria_list.append($new_criteria_item);
        }

    });

    $('.btn-new-criteria').click(function (e) {
        e.stopPropagation();
       
        var $this = $(this);
        $this.toggleClass('clicked');
        //$('ul.new-criteria.options-dd').removeClass('visible');
        SearchResume.hideOptionsDropDown($('ul.new-criteria.options-dd'));
        var $criteria_list = $this.next('ul.options-dd');
        // console.log($criteria_list);
        if ($this.hasClass('clicked')) {
            $('.btn-new-criteria').not($this).removeClass('clicked');
            // $criteria_list.addClass('visible');
            SearchResume.toggleOptionsDropDown($criteria_list);
        } else {
            // $criteria_list.removeClass('visible');
            SearchResume.hideOptionsDropDown($criteria_list);
        }
    });

    $('body').click(function (e) {

        $('a.btn-new-criteria').removeClass('clicked');
        // $('ul.options-dd').removeClass('visible');
        SearchResume.hideOptionsDropDown($('ul.options-dd'));

    });

    $('ul.new-criteria.options-dd').not('.change-criteria').find('li > a').click(function (e, param) {

        e.stopPropagation();

        //hide this options dd
        SearchResume.hideOptionsDropDown($(this).closest('.options-dd'));

        var selected_criteria_key = $(this).parent().attr('data-criteria-key');
        var selected_criteria = search_groups[selected_criteria_key];

        var $new_query_block = $('.query-block-sgl.template').clone().removeClass('template');
        changeQueryBlockState($new_query_block, 'incomplete', param);
        $new_query_block.find('a.btn-change-criteria-group')
			.attr('data-criteria-key', selected_criteria_key)
			.find('img').attr('src', selected_criteria.icon);

        $new_query_block.find('a.btn-change-criteria-group')
			.click(function (e) {
			    e.stopPropagation();
			    var $current_options_dd = $(this).next('ul.change-criteria.options-dd');
			    // console.log($('.options-dd.visible').not($(this).next('ul.change-criteria.options-dd')[0]));
			    // $('.options-dd.visible').not($current_options_dd[0]).removeClass('visible');
			    SearchResume.hideOptionsDropDown($('.options-dd.visible').not($current_options_dd[0]));
			    // $current_options_dd.toggleClass('visible');
			    SearchResume.toggleOptionsDropDown($current_options_dd);
			})
			.attr('data-criteria-key', selected_criteria_key)
			.find('img').attr('src', selected_criteria.icon);

        $new_query_block.find('ul.change-criteria.options-dd > li > a').click(function (e) {

            e.stopPropagation();

            var $this = $(this);

            var selected_criteria_key = $this.parent().attr('data-criteria-key');
            var selected_criteria = search_groups[selected_criteria_key];

            $this.closest('.query-block-sgl').find('a.btn-change-criteria-group')
				.attr('data-criteria-key', selected_criteria_key)
				.find('img').attr('src', selected_criteria.icon);


            $field_container = $this.closest('.query-block-sgl').find('.field-container');

            populateFieldOptions(selected_criteria, $field_container);

            // $this.closest('ul.options-dd').toggleClass('visible');
            SearchResume.toggleOptionsDropDown($this.closest('ul.options-dd'));

        });

        $new_query_block.find('a.btn-change-field').click(function (e) {
            e.stopPropagation();
            $current_options_dd = $(this).next('ul.change-field.options-dd');
            console.log($current_options_dd);
            // $('.options-dd.visible').not($current_options_dd[0]).removeClass('visible');
            SearchResume.hideOptionsDropDown($('.options-dd.visible').not($current_options_dd[0]));
            // $current_options_dd.toggleClass('visible');
             
            SearchResume.toggleOptionsDropDown($current_options_dd);
        });

        $new_query_block.find('a.btn-change-operator').click(function (e) {
            e.stopPropagation();
            var $current_options_dd = $(this).next('ul.change-operator.options-dd');
            console.log($current_options_dd);
            // $('.options-dd.visible').not($current_options_dd[0]).removeClass('visible');
            SearchResume.hideOptionsDropDown($('.options-dd.visible').not($current_options_dd[0]));

            // $current_options_dd.toggleClass('visible');
            SearchResume.toggleOptionsDropDown($current_options_dd);

        });
        $new_query_block.find('.input-container > .input-value').click(function () {
            var $value = $.trim($(this).text());
            if ($value != '') {
                var $query_block = $(this).closest('.query-block-sgl');
                //$query_block.find('.input-container > input').val($value);
                changeQueryBlockState($query_block, 'incomplete', param);
                $(this).hide();

            }
        });

        $new_query_block.find('.input-container > input').blur(function (e) {
            e.stopImmediatePropagation();
            var $value = $.trim($(this).val());
            if ($value != '') {
                var $query_block = $(this).closest('.query-block-sgl');
                $query_block.find('.input-container > .input-value').text($value);
                changeQueryBlockState($query_block, 'complete', param);
            }
        });

        // delete query button handler
        $new_query_block.find('.btn-delete-query').click(function (e) {

            e.stopPropagation();

            $(this).closest('.content').find('.new-criteria-container').removeClass('incomplete');
            var $qbgroup = $(this).closest('.query-block-group');
            $(this).closest('.query-block-sgl').remove();
            SearchResume.removeInvalidGroups($qbgroup);
            SearchResume.SendDataToServer();
        });


        $new_query_block.insertBefore($(this).closest('.content').find('.new-criteria-container'));

        $field_container = $new_query_block.find('.field-container');

        populateFieldOptions(selected_criteria, $field_container, param);

    });

    function populateFieldOptions(objcriteria, $field_container, param) {

        changeQueryBlockState($field_container.closest('.query-block-sgl'), 'incomplete', param);

        $field_container
			.find('ul.change-field.options-dd > li')
			.not('.template')
			.remove();

        $field_container
			.find('a.btn-change-field > span.field-label')
			.text('Select Field');

        $field_container.next('.operator-container').hide();

        if (objcriteria.fields) {

            for (field_key in objcriteria.fields) {
                var objfield = objcriteria.fields[field_key];
                console.log(objfield);
                var $new_field_item = $field_container.find('ul.change-field.options-dd > li.template').clone().removeClass('template');
                
                $new_field_item.attr('data-field-key', field_key);
                
                
                $new_field_item.find('span.label').text(objfield.display);
                
                $field_container.find('ul.options-dd').append($new_field_item);
            }

            $field_container.find('ul.change-field.options-dd > li > a').click(function (e, param) {
               
                e.stopPropagation();
                var $this = $(this);
                var selected_field_key = $this.parent().attr('data-field-key');
                
                console.log(selected_field_key);
                var objfield = objcriteria.fields[selected_field_key];
                $this.closest('.field-container').find('a.btn-change-field > span.field-label').text(objfield.display);
                $this.closest('.field-container').find('a.btn-change-field > span.field-label').attr("value", selected_field_key);
                
                $operator_container = $this.closest('.query-block-sgl').children('.operator-container');
                populateOperatorOptions(objfield, $operator_container, param);
                
                $field_container.parent().find('.additional-qualifiers-container').not('.template').remove();

                //populate additional qualifiers if present
                if ('additional_qualifiers' in objfield) {
                    var arrAdditionalQualifiers = objfield.additional_qualifiers;
                    var objAdditionalQualifiers = null;
                    var $new_additional_qualifiers = null;
                    var $all_qualifiers = null;
                    var $operator_container = null;
                    var $operator_options_dd = null;
                    var $select_options_dd = null;
                    var $new_item = null;
                    var $new_select_option = null;
                    var select_options = null;

                    for (var i = 0 ; i < arrAdditionalQualifiers.length ; i++) {

                        objAdditionalQualifier = arrAdditionalQualifiers[i];
                        $new_additional_qualifier = $('.additional-qualifiers-container.template').clone().removeClass('template');
                        $new_additional_qualifier.attr('data-qualifier-key', objAdditionalQualifier.key);
                        $new_additional_qualifier.find('.qualifier-name').text(objAdditionalQualifier.display);
                        $operator_container = $new_additional_qualifier.find('.operator-container');
                        $operator_options_dd = $operator_container.find('ul.change-operator.options-dd');
                        
                        for (var op_index = 0 ; op_index < objAdditionalQualifier.operators.length ; op_index++) {
                            
                            
                            console.log(objAdditionalQualifier.operators[op_index]);

                            $new_item = $operator_options_dd.find('li.template').clone().removeClass('template');
                            $new_item.attr('data-operator-key', objAdditionalQualifier.operators[op_index]);
                            $new_item.find('.label').text(objAdditionalQualifier.operators[op_index]);
                            console.log($new_item.find('.label').text());
                            $operator_options_dd.append($new_item);
                            

                        }
                        
                        $operator_container.find('a.btn-change-operator').click(function (e) {
                            e.stopPropagation();
                            var $current_options_dd = $(this).next('ul.change-operator.options-dd');
                            // console.log($('.options-dd.visible').not($(this).next('ul.change-criteria.options-dd')[0]));
                            // $('.options-dd.visible').not($current_options_dd[0]).removeClass('visible');
                            SearchResume.hideOptionsDropDown($('.options-dd.visible').not($current_options_dd[0]));
                            // $current_options_dd.toggleClass('visible');
                            SearchResume.toggleOptionsDropDown($current_options_dd);
                        });

                        $select_options_dd = $operator_container.find('ul.select-value.options-dd');


                        // console.log('---------------------------------------');

                        for (var sel_key in objAdditionalQualifier.select.options) {
                           
                            console.log(objAdditionalQualifier.select.options[sel_key]);
                            $new_select_option = $select_options_dd.find('li.template').clone().removeClass('template');
                            $new_select_option.attr('data-select-key', sel_key);
                            $new_select_option.find('.label').text(objAdditionalQualifier.select.options[sel_key]);
                            $select_options_dd.append($new_select_option);
                        }

                        //set default
                        $operator_container.find('.select-value-container > a.btn-change-selected-value > .selected-value-label').text(objAdditionalQualifier.select.options[objAdditionalQualifier.select.default]);

                        $operator_container.find('.select-value-container > a.btn-change-selected-value').click(function (e) {
                            e.stopPropagation();
                            var $current_options_dd = $(this).next('ul.select-value.options-dd');
                            // console.log($('.options-dd.visible').not($(this).next('ul.change-criteria.options-dd')[0]));
                            // $('.options-dd.visible').not($current_options_dd[0]).removeClass('visible');
                            SearchResume.hideOptionsDropDown($('.options-dd.visible').not($current_options_dd[0]));
                            // $('ul.new-criteria.options-dd').removeClass('visible');
                            // $current_options_dd.toggleClass('visible');
                            SearchResume.toggleOptionsDropDown($current_options_dd);
                        });

                        $operator_container.toggleClass('visible');

                        if (i == 0) {
                            $all_qualifiers = $('<div style="clear:both"></div>');
                        }

                        $all_qualifiers.after($new_additional_qualifier);

                    }

                    $all_qualifiers.find('ul.change-operator.options-dd > li > a').click(function (e) {
                        e.stopPropagation();
                        var $this = $(this);
                        
                        $this.closest('.operator-container').find('.btn-change-operator > .operator-label').text($this.children('.label').text());
                        // $this.closest('.change-operator.options-dd').toggleClass('visible');
                        SearchResume.toggleOptionsDropDown($this.closest('.change-operator.options-dd'));
                    });

                    $all_qualifiers.find('ul.select-value.options-dd > li > a').click(function (e) {
                        e.stopPropagation();
                        var $this = $(this);
                        $this.closest('.select-value-container').find('.btn-change-selected-value > .selected-value-label').text($this.children('.label').text());
                        // $this.closest('ul.select-value.options-dd').toggleClass('visible');
                        SearchResume.toggleOptionsDropDown($this.closest('ul.select-value.options-dd'));
                    });

                    $this.closest('.query-block-sgl').append($all_qualifiers);

                }

                // $this.closest('ul.change-field.options-dd').toggleClass('visible');
                SearchResume.toggleOptionsDropDown($this.closest('ul.change-field.options-dd'));
            });

        }

    }

    // function populateAdditionalQualifiers( arrQualifiers,  ) {

    // }

    function populateOperatorOptions(objfield, $operator_container, param) {

        changeQueryBlockState($operator_container.closest('.query-block-sgl'), 'incomplete', param);

        $operator_container
			.find('ul.change-operator.options-dd > li')
			.not('.template')
			.remove();

        $operator_container
			.find('a.btn-change-operator')
			.removeClass('selected')
			.find('span.operator-label')
			.text('Select Operator');

        $operator_container
			.find('.input-container')
			.find('label.input-value')
			.text('')
			.parent()
			.find('input')
			.val('')
			.attr('name', objfield.input.name)
			.parent().hide();


        console.log('operator_container', $operator_container);

        if (objfield.operators) {

            for (index in objfield.operators) {
                var hideintOp= false;
                objfield.type = objfield.type.toLowerCase();
              
                if (objfield.type == "datetime" && SearchResume.DateTimeFOP.indexOf(parseInt(index)) <= -1) {
                    continue;
                }
                if (objfield.type == "count" && SearchResume.CountFOP.indexOf(parseInt(index)) <= -1) {
                    continue;
                }

                //11 -Less then
                //12 - greater then
                if (objfield.type == "int" && SearchResume.IntFOP.indexOf(parseInt(index)) <= -1) {
                    continue;
                }
                //10-"equals" Operator
                if (objfield.type == "bit" && index != 10) {
                    continue;
                }

              
                if (objfield.type == "number" && SearchResume.NumberOP.indexOf(parseInt(index)) <= -1) {
                    continue;
                }
               

                if (objfield.type == "contains" && SearchResume.containsOP.indexOf(parseInt(index)) <= -1) {
                    continue;
                }
                
                if (objfield.type == "priority" && SearchResume.PriorityOperator.indexOf(parseInt(index)) <= -1) {
                    continue;
                }

                if ((objfield.type == '' || objfield.type == 'select') && (SearchResume.DateTimeFOP.indexOf(parseInt(index)) >= 0 || SearchResume.IntFOP.indexOf(parseInt(index)) >= 0 || SearchResume.PriorityOperator.indexOf(parseInt(index)) >= 0))
                { continue; }

                var operator_name = objfield.operators[index];
                console.log(operator_name);
                var $new_operator_item = $operator_container.children('ul.change-operator.options-dd').find('li.template').clone().removeClass('template');
                console.log('new_operator_item', $new_operator_item);
                $new_operator_item.attr('data-operator-key', operator_name);
                $new_operator_item.find('span.label').text(operator_name);
                $operator_container.find('ul.options-dd').append($new_operator_item);

            }

            $operator_container.find('.input-container > input').attr('name', objfield.input.name);


            $operator_container.find('ul.change-operator.options-dd > li > a').click(function (e, param,IsDataContain) {
                
                e.stopPropagation();
                var $this = $(this);

                if (IsDataContain != true) {
                    changeQueryBlockState($this.closest('.query-block-sgl'), 'incomplete', param);
                }

                $this.closest('.operator-container')
                    .find('.input-container')
                    .find('label.input-value')
                    .text('')
                    .parent()
                    .find('input')
                    .val('')
                    .attr('name', objfield.input.name)
                    .parent().hide();


                var selected_operator = $this.parent().attr('data-operator-key');
                console.log(selected_operator);
                $this.closest('.operator-container').find('.input-container').find('input').attr('list', '');
               
                if (IsDataContain != true) {
                    if (objfield.type != "select" && objfield.type != "datetime" && (objfield.operators.indexOf(selected_operator) == 2 || objfield.operators.indexOf(selected_operator) == 5)) {
                        var dataList = $('#selectval');
                        var field = objfield.fieldName;
                        $.post("SearchResume/GetContainsDrpValue", { field: field }, function (data) {

                            data = JSON.parse(data);

                            dataList.empty();
                            if (data.length > 0) {
                                for (var i = 0, len = data.length; i < len; i++) {

                                    var opt = $("<option>" + data[i].TextField + "</option>").attr("value", data[i].ValueField);
                                    dataList.append(opt);
                                }
                                $this.closest('.operator-container').find('.input-container').find('input').attr('list', 'selectval');
                            }

                        });
                    }
                }
                $this.closest('.operator-container').find('a.btn-change-operator')
					.addClass('selected')
					.find('span.operator-label')
					.text(selected_operator);
                // $this.closest('ul.change-operator.options-dd').toggleClass('visible');
                SearchResume.toggleOptionsDropDown($this.closest('ul.change-operator.options-dd'));
                if (IsDataContain != true) {
                    if (objfield.type.toLowerCase() == "bit" || objfield.type.toLowerCase() == "count") {
                        var dataList = $('#selectval');
                        var field = objfield.fieldName;
                        $.post("SearchResume/GetDrpValueByType", { type: objfield.type }, function (data) {
                            data = JSON.parse(data);
                            dataList.empty();
                            if (data.length > 0) {
                                for (var i = 0, len = data.length; i < len; i++) {
                                    var opt = $("<option>" + data[i].TextField + "</option>").attr("value", data[i].ValueField);
                                    dataList.append(opt);
                                }
                                $this.closest('.operator-container').find('.input-container').find('input').attr('list', 'selectval');
                            }

                        });
                    }

                    if (objfield.type == "select" || objfield.type == "priority") {

                        var dataList = $('#selectval');
                        var field = objfield.fieldName;
                        $.post("SearchResume/GetDrpValue", { field: field }, function (data) {
                            data = JSON.parse(data);
                            dataList.empty();
                            if (data.length > 0) {
                                for (var i = 0, len = data.length; i < len; i++) {
                                    var opt = $("<option>" + data[i].TextField + "</option>").attr("value", data[i].TextField);
                                    dataList.append(opt);
                                }
                                $this.closest('.operator-container').find('.input-container').find('input').attr('list', 'selectval');
                            }

                        });
                        //var drpData = SearchResume.postBasedOnField(field, 0, dataList);


                    }

                    if (objfield.type == "datetime" && SearchResume.DateTimeDaysOP.indexOf(objfield.operators.indexOf(selected_operator)) >= 0) {

                        var dtclass = $this.closest('.operator-container').find('.input-container').find('input').hasClass('hasDatepicker');

                        if (!dtclass) {
                            var dtId = $this.closest('.operator-container').find('.input-container').find('input').attr('id') + objfield.input.name;
                            dtId += $('#search-resumes').find('input[id^=' + dtId + ']').length
                            $this.closest('.operator-container').find('.input-container').find('input').attr('id', dtId);

                            $ctr = $this.closest('.operator-container').find('.input-container').find('#' + dtId);

                            $ctr.datepicker({
                                changeYear: true,
                                changeMonth: true,
                                onSelect: function (dateText) {
                                    $(this).blur();
                                }
                            });

                        }
                    }
                    else {

                        $this.closest('.operator-container').find('.input-container').find('input').datepicker("destroy");
                    }

                    if (SearchResume.OperatorName.indexOf(selected_operator) == -1) {

                        $this.closest('.operator-container').find('.input-container').show();
                        $operator_container.find('.input-container').css('float', 'left');
                        if (param != 'auto')
                            $operator_container.find('.input-container > input').first().focus();
                    }
                    else {
                        $operator_container.find('.input-container > input').first().val('null');
                        $operator_container.find('.input-container > input').first().trigger("blur", ["auto"]);
                    }
                }
            });
            
            $operator_container.show();

        }

    }

    function changeQueryBlockState($query_block, state, param) {

        switch (state) {

            case 'complete':

                $query_block.removeClass('query-incomplete').addClass('query-complete groupable');
                $query_block.closest('.content').find('.new-criteria-container').removeClass('incomplete');

                //becomes draggable
                SearchResume.makeDraggable($query_block);

                // send query to server

                SearchResume.SendDataToServer();

                break;

            case 'incomplete':

                $query_block.removeClass('query-complete groupable').addClass('query-incomplete');
                $query_block.closest('.content').find('.new-criteria-container').addClass('incomplete');


                if (param == undefined) {
                    $('#accordion').children('.acc-header').each(function (index) {
                        $(this).children('.prfle-tab-head').children('span').html('');
                    });
                }
                //remove Draggable
                SearchResume.removeDraggable($query_block);

                break;
        }
    }

    $('.content').on('click', '.groupable', function (e) {

        e.stopPropagation();

        console.log($('.selected-for-grouping.groupable').parent()[0]);
        console.log($(this).parent()[0]);

        var $current_selected = $('.selected-for-grouping.groupable');

        var $this = $(this);

        if ($current_selected.length == 0 || $current_selected.parent()[0] == $(this).parent()[0]) {

            $(this).toggleClass('selected-for-grouping');

            var $toGroup = $('.selected-for-grouping.groupable');

            if ($toGroup.length > 1) {
                createGroupAfterDelay($toGroup);
            }
        }

    });

    $('.content').on('mouseenter mouseleave', '.groupable,.options-dd', function (e) {

        // e.stopPropagation();

        $('.groupable.hover').removeClass('hover');

        e.type === 'mouseenter' ? ($(this).hasClass('groupable') ? $(this).addClass('hover') : false) : $(this).parent().closest('.groupable:hover').addClass('hover');

    });

    $('#search-resumes a.action-exclude-result').click(function () {
        $(this).closest('.search-row').toggleClass('exclude-result');
    });

    function createGroupAfterDelay($toGroup, delay) {

        if (typeof delay === 'undefined') {
            delay = 1000;
        }

        console.log('delay=', delay);

        //check for existing timer
        if (grouptimer) {

            window.clearTimeout(grouptimer);

        }

        var $parent = $toGroup.parent();

        if ($parent.hasClass('query-blocks-container')) {
            //parent is group, check if it has another child which is not part of the $toGroup collection
            if ($parent.children('.groupable').length > $toGroup.length) {
                //create new group and append to parent
                grouptimer = window.setTimeout(function () {
                    SearchResume.createNewGroup($toGroup);
                }, delay);
            }
        } else {
            //parent not a group, we can create new group
            //create new group and append to parent
            grouptimer = window.setTimeout(function () {
                SearchResume.createNewGroup($toGroup);
            }, delay);
        }

    }

    $('.accordion > a.header').click(function () {
        var $current = $(this).next('.content');
        if ($current.parent().hasClass('expanded')) {
            return;
        }
        $('.accordion.expanded > .content').slideToggle(400, function () {
            $(this).parent().removeClass('expanded');
            $current.slideToggle('medium', function () {
                $(this).parent().addClass('expanded');
            });
        });

    });

    SearchResume.removeDraggable = function ($queryblock) {

        if ($queryblock.hasClass('ui-draggable')) {
            $queryblock.draggable("destroy");
        }

        console.log('removeDraggable');
    }

    SearchResume.removeGrouping = function ($group_container) {

        console.log($group_container);

        var $groupable = $group_container.children('.query-blocks-container').children('.groupable');

        var $sibling_groups = $group_container.siblings('.query-block-group');
        if ($sibling_groups.length > 0) {
            $sibling_groups.last().after($groupable);
        } else {
            $group_container.parent().prepend($groupable);
        }
        $group_container.remove();
    }
    SearchResume.makeDroppable($('.content'));

    SearchResume.getHead = function (t, str) {
        var s = '';

        var component = [];

        //if (t.Items.Count == 0) //get the condition
        //{
        //  return s=t.Header.ToString(); //change this to your real getCondition function.
        //}


        if (t.find('[class^="' + str + '"]').length == 0) {

            if (!$(t).hasClass("template")) {
                
                var criteria = t.find(".criteria-group-container").find("a:first").attr("data-criteria-key");
                var type = 'query-block-sgl';
                var field = t.find(".field-container").find("a:first").find("span:first").attr("value");
                
                var operators = t.find(".operator-container").find('[class^="btn-change-operator"]').find("span:first").text();
                var value = t.find(".operator-container").find(".input-container").find("label:first").text();
                
                
                var _Com = {};
                _Com.type = type;
                _Com.value = value;
                t.addClass("visited");
                var compo = '{"type":"' + type + '","criteria":"' + criteria + '","field":"' + field + '","operators":"' + operators + '","value":"' + value + '"},'

                return compo;
            }
        }
        else {
            t.find('[class^="' + str + '"]').each(function (index) {
                if ($(this).hasClass("query-block-sgl") && !$(this).hasClass("template") && !$(this).hasClass("visited")) {
                    //component.push(getHead($(this), "query-block-sgl"));
                    s += SearchResume.getHead($(this), "query-block-sgl");
                }
                else if ($(this).hasClass("query-block-group") && !$(this).hasClass("template") && !$(this).hasClass("visited")) {
                    var type = "query-group";
                    var grouping_operator = "OR";
                    if ($(this).hasClass("and"))
                        grouping_operator = "AND";


                    var oldcomponent = [];

                    var newCom = [];
                    var _groupStrStart = '{"type":"' + type + '","grouping_operator":"' + grouping_operator + '","components":[';
                    var _groupStrEnd = ']},';

                    if ($(this).children().find(".query-block-group").length <= 0) {
                        var d = SearchResume.getHead($(this), "query-block-sgl");
                        //component.push(d);
                        //s.push((d == "()") ? {} : (" " + grouping_operator + " " + d));

                        s += ((d == '') ? '' : (_groupStrStart + d.slice(0, -1) + _groupStrEnd));

                        //s += ((d == "") ? "" : (" " + grouping_operator + " " + d));
                    }
                    else {
                        var d = SearchResume.getHead($(this), "query-block-");
                        //component.push(d);

                        //newCom.push(d);
                        //component = [];
                        //oldcomponent.pop().component.push(d);

                        //component.push(oldcomponent.pop());
                        //s.push(((d == "()") ? {} : (" " + grouping_operator + " " + d)));
                        //s += ((d == "()") ? {} : (" " + grouping_operator + " " + d));

                        s += ((d == '') ? '' : (_groupStrStart + d.slice(0, -1) + _groupStrEnd));
                    }
                    //oldcomponent.push({
                    //  type: type,
                    // grouping_operator: grouping_operator,
                    //component:component
                    //});
                }
            });
            return s;//group sub conditions
            //return Com;//group sub conditions
            //return component;
        }
    }
}
SearchResume.loadSavedSearch = function (_saved_data) {


    $(".inclusions").find('[class^="query-block-"]').each(function (index) {
        if (!$(this).hasClass("template")) {
            $(this).remove();
        }
    });

    $(".exclusions ").find('[class^="query-block-"]').each(function (index) {
        if (!$(this).hasClass("template")) {
            $(this).remove();
        }
    });
    var mycount = 0;
    for (key in _saved_data) {


        SearchResume.Isvisisted = true;
        var data = _saved_data[key];

        if (key.toString().match("_count$")) {
            mycount = _saved_data[key];
            //alert(mycount);
        }
        if (data.length > 0) {
            SearchResume.createQueryGroup(key, data);
        }
        $("." + key).parents('.acc-inner').parent().prev().find('.prfle-tab-head > span').html('(' + VacancyModel.commaSeparateNumberByValue(mycount) + ')');
    }
}

SearchResume.createQueryGroup = function (inclusion_exclusion, components, grouping_operator, grpCount) {
    var $created_components = [];

    for (var i = 0 ; i < components.length ; i++) {
        var temp_obj = components[i];
        //var obj = _components[i];
        switch (temp_obj.type) {
            case 'query-block-sgl':
                if (temp_obj.field != 'undefined') {
                    console.log('----------- query block ----------', temp_obj.field);
                    //select criteria and create new query block
                    //$('.' + inclusion_exclusion + ' .new-criteria-container ul.new-criteria > li[data-criteria-key="' + temp_obj.criteria + '"] > a').click();
                    $('.' + inclusion_exclusion + ' .new-criteria-container ul.new-criteria > li[data-criteria-key="' + temp_obj.criteria + '"] > a').trigger("click", ["auto"]);
                    var $new_query_block = $('.query-block-sgl.query-incomplete');
                    
                    //select field
                    $new_query_block.find('.field-container > ul.change-field > li[data-field-key="' + temp_obj.field + '"] > a').trigger("click", ["auto"]);
                    // $new_query_block.find('.field-container > ul.change-field').toggleClass('visible');
                    SearchResume.toggleOptionsDropDown($new_query_block.find('.field-container > ul.change-field'));

                    //select operator
                    if (SearchResume.OperatorName.indexOf(temp_obj.operators) == -1) {
                        $new_query_block.find('.operator-container > ul.change-operator > li[data-operator-key="' + temp_obj.operators + '"] > a').trigger("click", ["auto"]);
                    }
                    else {
                        $new_query_block.find('.operator-container > ul.change-operator > li[data-operator-key="' + temp_obj.operators + '"] > a').trigger("click", ["auto", true]);
                    }
                    // $new_query_block.find('.operator-container > ul.change-operator').toggleClass('visible');
                    SearchResume.toggleOptionsDropDown($new_query_block.find('.operator-container > ul.change-operator'));

                    // set value
                    $new_query_block.find('.operator-container > .input-container > label.input-value').text(temp_obj.value);

                    //$new_query_block.find('.operator-container > .input-container > input').blur();

                    var $query_block = $new_query_block.find('.operator-container > .input-container > input').closest('.query-block-sgl');
                    $query_block.removeClass('query-incomplete').addClass('query-complete groupable');
                    $query_block.closest('.content').find('.new-criteria-container').removeClass('incomplete');

                    SearchResume.makeDraggable($query_block);

                    $new_query_block.find('.matches > span.count').text(VacancyModel.commaSeparateNumberByValue(temp_obj.count));

                    if (temp_obj.additional_qualifiers != null) {
                        if ('additional_qualifiers' in temp_obj && temp_obj.additional_qualifiers.length > 0) {

                            for (var j = 0 ; j < temp_obj.additional_qualifiers.length ; j++) {
                                var qualifier = temp_obj.additional_qualifiers[j];
                                var $qualifier_container = $new_query_block.find('div[data-qualifier-key="' + qualifier.key + '"].additional-qualifiers-container');

                                // console.log('+++++++++++', $qualifier_container);

                                //select operator
                                $qualifier_container.find('ul.change-operator.options-dd > li[data-operator-key="' + qualifier.operator + '"] > a').click();
                                // $qualifier_container.find('ul.change-operator.options-dd').toggleClass('visible');
                                SearchResume.toggleOptionsDropDown($qualifier_container.find('ul.change-operator.options-dd'));

                                //select value
                                $qualifier_container.find('ul.select-value.options-dd > li[data-select-key="' + qualifier.option + '"] > a').click();
                                // $qualifier_container.find('ul.select-value.options-dd').toggleClass('visible');
                                SearchResume.toggleOptionsDropDown($qualifier_container.find('ul.select-value.options-dd'));
                            }
                        }
                    }
                    $created_components.push($new_query_block);
                }
                break;
            case 'query-group':
                var $newQueryGroup = SearchResume.createQueryGroup(inclusion_exclusion, temp_obj.Components, temp_obj.grouping_operator, temp_obj.count);


                if ($newQueryGroup !== false) {
                    $created_components.push($newQueryGroup);
                }
                break;
        }

    }

    //return the new created group
    if ($created_components.length > 0) {



        //create group
        if (grouping_operator != undefined) {

            for (var ctr = 0 ; ctr < $created_components.length ; ctr++) {
                $created_components[ctr].addClass('selected-for-grouping');
            }

            SearchResume.createNewGroup($('.selected-for-grouping'), grouping_operator, true, grpCount);
            var $newGroup = $($created_components[0].parent()).closest('.query-block-group');
            //var $newGroup = $created_components[0].closest('.query-block-group');

            return $newGroup;
        }
        else { return false; }

    }

    return false;

}

SearchResume.createNewGroup = function ($toGroup, grouping_operator, isAutomation, count) {

    // find the destination, where the new quresy-block-group will be attached
    $container = $toGroup.filter('.groupable:not(:has(.selected-for-grouping))').parent();

    console.log('new group container', $container);

    var $new_group = $('.query-block-group.template').clone().removeClass('template').addClass('groupable');
    $new_group.find('.query-blocks-container').append($toGroup);
    $new_group.find('.groupable').removeClass('selected-for-grouping');
    $new_group.css('display', 'inline-block');
    $new_group.children(":last").find('span.count').text(VacancyModel.commaSeparateNumberByValue(count));
    var $button = $new_group.children('.btn-select-grouping-container').find('a.btn-select-grouping');

    $button.click(function (e) {
        e.stopPropagation();
        // $(this).next('ul.options-dd.grouping-type').toggleClass('visible');
        SearchResume.toggleOptionsDropDown($(this).next('ul.options-dd.grouping-type'));
    });

    $button.next('ul.options-dd').find('li > a').click(function (e) {

        e.stopPropagation();
        switch ($(this).attr('class')) {
            case 'and':
            case 'or':
                $button.closest('.query-block-group').removeClass('and or').addClass($(this).attr('class'));
                $button.text($(this).text());

                break;
            case 'remove-grouping':
                SearchResume.removeGrouping($button.closest('.query-block-group'));

                break;
        }
        $button.click();
        if (isAutomation != true) {

            SearchResume.SendDataToServer();
        }
        if (isAutomation == true) {
            isAutomation = false;
        }
    });

    var $last_query_group = $container.children('.query-block-group:last');

    console.log($last_query_group);

    if ($last_query_group.length > 0) {
        $last_query_group.after($new_group);
    } else {
        $container.prepend($new_group);
    }

    // set grouping operator, if passed
    if (typeof grouping_operator !== 'undefined') {

        $new_group.children('.btn-select-grouping-container').find('ul.options-dd.grouping-type > li > a.' + grouping_operator.toLowerCase()).click();

        // $new_group.children('.btn-select-grouping-container').find('ul.options-dd.grouping-type').toggleClass('visible');
        SearchResume.toggleOptionsDropDown($new_group.children('.btn-select-grouping-container').find('ul.options-dd.grouping-type'));
    }

    //becomes draggable and droppable
    SearchResume.makeDraggableAndDroppable($new_group);

    //delete query group button handler
    $new_group.find('.btn-delete-query-group').click(function (e) {

        e.preventDefault();
        e.stopPropagation();

        var $qbgroup_container_qbgroup = $(this).parent().parent().closest('.query-block-group');
        $(this).closest('.query-block-group').remove();

        SearchResume.removeInvalidGroups($qbgroup_container_qbgroup);
        SearchResume.SendDataToServer();

    });

    // set * in the vertical middle		
    SearchResume.fixGroupingOperatorPosition($button);

}

SearchResume.CreateSerializeObj = function () {
    var SavedData = '';

    SavedData = SearchResume.GetSavedData();

    var JsonStrignify = JSON.stringify(SavedData);

    var SearchParameters = "{\"SearchParameters\":" + JsonStrignify + "}";

    return SearchParameters;

}

SearchResume.SendDataToServer = function () {

    var SearchParameters = SearchResume.CreateSerializeObj();

    $.post("SearchResume/GetData", { data: SearchParameters }, function (data) {
        data = JSON.parse(data);
        var objarr1 = [];
        
        $(".search-results").parents('.acc-inner').parent().prev().find('.prfle-tab-head > span').html('(' + VacancyModel.commaSeparateNumberByValue(data.SearchResultCount) + ')');

        array = '[' + data.jsonString + ']';
        var myobj = JSON.parse(array);
        objarr1.push(myobj);
        SearchResume.loadSavedSearch(myobj[0].SearchParameters[0]);
    });
}
SearchResume.GetSavedData = function () {

    SearchResume.ChangeVisitedNode($('.inclusions > .content'));
    SearchResume.ChangeVisitedNode($('.exclusions > .content'));
    var _inclusions = SearchResume.getHead($('.inclusions > .content'), "query-block-");
    var _exclusions = SearchResume.getHead($('.exclusions > .content'), "query-block-");

    var _QueryArr = [];

    _exclusions = '[' + _exclusions.slice(0, -1) + ']';
    _inclusions = '[' + _inclusions.slice(0, -1) + ']';

    var _json_inclusions = JSON.parse(_inclusions);
    var _json_exclusions = JSON.parse(_exclusions);


    _QueryArr.push(_json_exclusions);
    _QueryArr.push(_json_inclusions);

    var SearchParameters = [];

    SearchParameters.push({
        inclusions: _QueryArr.pop(),
        exclusions: _QueryArr.pop()
    });

    return SearchParameters;
}
SearchResume.ChangeVisitedNode = function (t) {
    t.find('[class^="query-block-"]').each(function (index) {

        if (!$(this).hasClass("template") && $(this).hasClass("visited")) {
            $(this).removeClass("visited");
        }
        //var groupId = 0;
        //if (!$(this).hasClass("template") && $(this).hasClass("query-block-group")) {
        //    groupId++;
        //    if (!$(this).hasClass("template") && $(this).hasClass("query-block-sgl")) {
        //        groupId = groupId+0.1
        //    }
        //}
    });
}

SearchResume.makeDraggableAndDroppable = function ($element) {

    SearchResume.makeDraggable($element);

    SearchResume.makeDroppable($element);

}

SearchResume.makeDraggable = function ($element) {

    $element.draggable({
        cursor: 'crosshair',
        revert: 'invalid',
        zIndex: 10000
    });
}

SearchResume.makeDroppable = function ($element) {

    $element.droppable({
        tolerance: 'pointer',
        accept: '*',
        revert: 'invalid',
        greedy: true,
        over: function (event, ui) {
            $('.ui-droppable.over').removeClass('over');
            $(this).addClass('over');
        },
        out: function (event, ui) {
            console.log($(this));
            $(this).parent().closest('.query-block-group, .content').addClass('over');
            // $(this).parent('.content').addClass('over');
            $(this).removeClass('over');
        },
        drop: function (event, ui) {

            // console.log('target = ', event.target);
            var $dropped = $(ui.draggable);
            var $target = $(this);
            var $dropped_container = $dropped.parent().closest('.query-block-group');

            // console.log('draggable', $dropped);

            var src_container_id = 'src_container_id';

            if ($(event.target).attr('id') === src_container_id) {

                return;
            }

            // console.log('drop target', $target);

            var $append_target = $target;
            if ($target.hasClass('query-block-group')) {
                $append_target = $target.children('.query-blocks-container');
                $dropped.detach().css({ left: 0, top: 0, 'z-index': 100 }).appendTo($append_target);
            } else {
                console.log($target);
                $append_target = $target.children('.new-criteria-container');
                $dropped.detach().css({ left: 0, top: 0, 'z-index': 100 }).insertBefore($append_target);
            }

            //update grouping op vertical position for target group
            SearchResume.fixGroupingOperatorPosition($target.children('.btn-select-grouping-container').find('a.btn-select-grouping'));
            //for source group
            SearchResume.fixGroupingOperatorPosition($dropped_container.children('.btn-select-grouping-container').find('a.btn-select-grouping'));

            // check if dropped source container has <= 1 query-block-sgl/query-block-group
            SearchResume.removeInvalidGroups($dropped_container);

            $target.removeClass('over');
            SearchResume.SendDataToServer();
        }
    });

}

SearchResume.fixGroupingOperatorPosition = function ($button) {

    // console.log('@@@@@@ button @@@@@@@@@@',$button);

    var top_offset = ($button.closest('.query-block-group').height() - $button.height()) / 2 + 'px';
    // console.log(top_offset);
    $button.parent().css('margin-top', top_offset);

}

SearchResume.hideOptionsDropDown = function ($dd_handle) {

    $dd_handle.removeClass('visible');
    $dd_handle.closest('.acc-Content').removeClass('overflow-visible');

}

SearchResume.toggleOptionsDropDown = function ($dd_handle) {
    if ($dd_handle.hasClass('visible')) {
        $dd_handle.removeClass('visible');
        $dd_handle.closest('.acc-Content').removeClass('overflow-visible');
    } else {
        $dd_handle.addClass('visible');
        console.log($dd_handle.closest('.acc-Content'));
        $dd_handle.closest('.acc-Content').addClass('overflow-visible');
    }
}

SearchResume.removeInvalidGroups = function ($qbgroup) {
    if ($qbgroup.length > 0) {
        var $query_blocks = $qbgroup.children('.query-blocks-container').children('.groupable');
        console.log('dropped_container', $qbgroup);
        switch ($query_blocks.length) {
            case 1:
                $qbgroup.attr('id', 'src_container_id');
                $qbgroup.replaceWith($qbgroup.children('.query-blocks-container').children('.groupable'));
                break;
            case 0:
                //remove this query block group, this wont ever happen. since we remove any group with just 1 node.
                $qbgroup.remove();
                break;
        }
    }
}
var grouptimer = null;

