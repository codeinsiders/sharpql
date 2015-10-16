// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemConstant.cs" company="CODE Insiders LTD">
// 
// Copyright 2013-2015 CODE Insiders LTD
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
// http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CodeInsiders.SharpQL
{
    public class SystemConstant : Expression
    {
        public string SqlScript { get; private set; }

        private SystemConstant(string sqlScript) {
            this.SqlScript = sqlScript;
        }

        public static SystemConstant CURRENT_USER {
            get {
                return new SystemConstant("CURRENT_USER");
            }
        }

        public static SystemConstant SYSTEM_USER {
            get {
                return new SystemConstant("SYSTEM_USER");
            }
        }

        public static SystemConstant CURRENT_TIMESTAMP {
            get {
                return new SystemConstant("CURRENT_TIMESTAMP");
            }
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SystemExpression(parent, this);
            
        }
    }
}