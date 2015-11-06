﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace e10.Shared.Util
{
    public static class ReflectionHelper
    {
        public static IList<Type> GetTypesThatImplementingType(Type type)
        {
            IList<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom)
                .ToList();
            return types;
        }

        public static IList<Type> GetEnumTypesInNamespace(string _namespace,string startsWith)
        {
            try
            {
                var mine = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith(startsWith)).SelectMany(s => s.GetTypes());
                return mine.Where(x =>
                    x.IsInNamespace(_namespace) && x.IsEnum)
                    .ToList();

            }
            catch(ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach(Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    var exFileNotFound = exSub as FileNotFoundException;
                    if(exFileNotFound != null)
                    {
                        if(!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                throw new Exception(sb.ToString());
            }
        }

        public static IList<Type> GetClassesThatImplementingType(Type type)
        {
            IList<Type> types = GetTypesThatImplementingType(type);
            types = types.Where(x => x.IsClass && !x.IsAbstract).ToList();
            return types;
        }

        public static Type GetModelByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("Assetry"))
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public static void HasDefaultValue(this ConventionPrimitivePropertyConfiguration model, bool what)
        {

        }
    }
}
