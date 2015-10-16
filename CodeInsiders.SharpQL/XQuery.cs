// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XQuery.cs" company="CODE Insiders LTD">
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
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Reflection;

    public class XQuery : XBatch
    {
        protected void WriteScript(string scriptName, object model = null) {
            var script = this.ReadResourceAsString(scriptName);
            this.ScriptBuilder.Write(script);
            if (model != null) {
                var args = this.GetParametersFromModel(model);
                foreach (var p in args) {
                    var sqlParam = new SqlParameter(p.Key, p.Value);
                    this.ScriptBuilder.AddSqlParameter(sqlParam);
                }
            }
        }

        protected string ReadResourceAsString(string resourceName) {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                if (stream == null) {
                    throw new InvalidOperationException("Script " + resourceName + " does not exists.");
                }

                using (StreamReader reader = new StreamReader(stream)) {
                    return reader.ReadToEnd();
                }
            }
        }

        protected Dictionary<string, object> GetParametersFromModel(object model) {
            var args = new Dictionary<string, object>();

            if (model == null) {
                return args;
            }

            var properties = model.GetType().GetProperties();
            foreach (PropertyInfo p in properties) {
                if (p.CanRead) {
                    var name = p.Name;
                    var value = p.GetValue(model);

                    args.Add(name, value);
                }
            }

            return args;
        }
    }
}