def import_csv_model(csv_path, net_object_id)
    puts csv_path
    puts net_object_id
    begin
        net = $ICM_VARIABLES[net_object_id.to_i]
        open_net = net.open
        open_net.csv_import(csv_path, {})
        open_net.validate("Base")
        open_net.commit "Imported CSV"
        open_net.close
        response = construct_response_hash_for_model_object(net)
    rescue => e
      response = error_response(e)
    end

    return response   
end

#Test
# db= WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
# mo= db.model_object_from_type_and_id('Model Network', 176)
# $ICM_VARIABLES = Hash.new
# $ICM_VARIABLES[mo.object_id.to_s] = mo
# obj_id= mo.object_id.to_s
# csv_path = "C:\\Users\\lakeshc\\OneDrive - Autodesk\\011_Dynamo\\003_Graph\\002 - Create Network\\Existing ICM Network.csv"
# puts import_csv_model(csv_path, obj_id)