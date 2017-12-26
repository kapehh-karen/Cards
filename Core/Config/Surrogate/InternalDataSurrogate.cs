using Core.Config.Surrogate.Data;
using Core.Data.Base;
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

namespace Core.Config.Surrogate
{
    public class InternalDataSurrogate : IDataContractSurrogate
    {
        public InternalDataSurrogate(DataBase runtimeDataBase)
        {
            Base = runtimeDataBase;
        }

        public DataBase Base { get; private set; }

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
            if (obj is FieldDataSurrogate fieldData)
            {
                return Base.Tables
                    .FirstOrDefault(t => t.Name.Equals(fieldData.TableName))
                    ?.Fields.FirstOrDefault(f => f.Name.Equals(fieldData.FieldName));
            }

            if (obj is TableDataSurrogate tableData)
            {
                return Base.Tables
                    .FirstOrDefault(t => t.Name.Equals(tableData.TableName));
            }

            return obj;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is FieldData fieldData)
            {
                var field = new FieldDataSurrogate()
                {
                    FieldName = fieldData.Name,
                    TableName = fieldData.ParentTable.Name
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