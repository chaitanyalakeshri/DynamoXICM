using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WSClass;
//using WSClass.WSOpenNetwork;

namespace ICMNodes
{
    public class Database
    {
        public static WSModelObject CreateModelGroup(string groupName, WSDatabase database)
        {
            string databaseID = database.RubyId.ToString();
            WSModelObject response = (WSModelObject)DynamoXICM.ExecuteFunction.Execute("create_model_group", new string[] { groupName, databaseID });
            if (response == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response to WSModelObject.");
            }
            return response;
        }

        public static WSModelObject CreateICMModel(string modelName, WSModelObject modelFolder)
        {
            if (modelFolder == null)
            {
                throw new ArgumentNullException(nameof(modelFolder), "Model folder cannot be null.");
            }
            if (modelFolder.Type != "Model Group")
            {
                throw new ArgumentException("WSModelObject must be of type 'Model Group'.", nameof(modelFolder));
            }

            string modelFolderId = modelFolder.RubyId.ToString();
            WSModelObject response = (WSModelObject)DynamoXICM.ExecuteFunction.Execute("create_icm_model", new string[] { modelName, modelFolderId });
            return response;
        }






    }
}
