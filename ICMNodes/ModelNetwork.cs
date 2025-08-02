using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WSClass;

namespace ICMNodes
{
    public class ModelNetwork
    {
        public static WSOpenNetwork OpenModel(WSModelObject model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Model cannot be null.");
            }
            if (model.Type != "Model Network")
            {
                throw new ArgumentException("WSModelObject must be of type 'Model Network'.", nameof(model));
            }
            string modelId = model.RubyId.ToString(); //Always use ICM object ID , not RubyId. Ruby object might expire once opened
            WSOpenNetwork response = (WSOpenNetwork)DynamoXICM.ExecuteFunction.Execute("open_model_network", new string[] { modelId });
            return response;
        }

        public static WSModelObject ImportCsvModel(string csvPath, WSModelObject ICMModel)
        {
            if (ICMModel == null)
            {
                throw new ArgumentNullException(nameof(ICMModel), "ICM model cannot be null.");
            }
            if (ICMModel.Type != "Model Network")
            {
                throw new ArgumentException("WSModelObject must be of type 'Model Network'.", nameof(ICMModel));
            }
            if (string.IsNullOrEmpty(csvPath))
            {
                throw new ArgumentNullException(nameof(csvPath), "CSV path cannot be null or empty.");
            }
            if (!System.IO.File.Exists(csvPath))
            {
                throw new FileNotFoundException("CSV file not found.", csvPath);
            }
            string modelId = ICMModel.RubyId.ToString();

            WSModelObject response = (WSModelObject)DynamoXICM.ExecuteFunction.Execute("import_csv_model", new string[] { csvPath, modelId });
            return response;
        }


        public static object GetModelObject(string rowId,WSOpenNetwork openNetwork, string tableName )
        {
            //Convert table name to a ruby class name
            //string wsTableName = WSClass.OpenNetwork.NameConverter.CSharpClassToRuby(tableName);

            string modelId = openNetwork.RubyId.ToString();

            object response = DynamoXICM.ExecuteFunction.Execute("get_row_object", new string[] {rowId, openNetwork.RubyId.ToString(), tableName });
            return response;
        }

    }

}
