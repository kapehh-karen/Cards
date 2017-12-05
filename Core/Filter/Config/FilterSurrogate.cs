using Core.Data.Field;
using Core.Data.Table;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Filter.Config
{
    public class FilterSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            if (typeof(FieldData).IsAssignableFrom(type))
                return typeof(FieldDataSurrogate);

            if (typeof(TableData).IsAssignableFrom(type))
                return typeof(TableDataSurrogate);

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            // TODO: Сделать привязку к DataBase

            if (obj is FieldDataSurrogate fieldData)
            {
                var field = new FieldData()
                {
                    Name = fieldData.FieldName
                };
                return field;
            }

            if (obj is TableDataSurrogate tableData)
            {
                var table = new TableData()
                {
                    Name = tableData.TableName
                };
                return table;
            }

            return obj;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            // TODO: Сделать привязку к DataBase

            if (obj is FieldData fieldData)
            {
                var field = new FieldDataSurrogate()
                {
                    FieldName = fieldData.Name
                };
                return field;
            }

            if (obj is TableData tableData)
            {
                var table = new TableDataSurrogate()
                {
                    TableName = tableData.Name
                };
                return table;
            }

            return obj;
        }

        #region NotImplemented

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}