﻿#region license
// Copyright (c) 2007-2010 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Linq;
using System.Linq.Expressions;
using SolrNet;

namespace ATS.WebUi.Helpers
{
    public static class IReadOnlyMappingManagerExtensions {
        public static string FieldName<T>(this IReadOnlyMappingManager mapper, Expression<Func<T, object>> property) {
            var propertyName = property.MemberName();
            return mapper.GetFields(typeof (T)).First(p => p.Key == propertyName).Key;
        }
    }
}