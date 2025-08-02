def get_database_object_from_type_and_id(db_object_id, object_type, object_id)
    begin
        db = $ICM_VARIABLES[db_object_id.to_i]
        mo=db.model_object_from_type_and_id(object_type, object_id)
        $ICM_VARIABLES[mo.object_id] = mo

        response = construct_response_hash_for_model_object(mo)
    rescue => e
        response = error_response(e)
    end
    
    return response
end


#Test
# db= WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"