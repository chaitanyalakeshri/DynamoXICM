def open_database(db_path)
    begin
        db = WSApplication.open(db_path)
        $ICM_VARIABLES[db.object_id.to_i] = db
    
        data = {
            "Path" => db.path,
            "Guid" => db.guid,
            "ResultRoot" => db.result_root,
            "ListReadWriteRunFields" => db.list_read_write_run_fields,
            "RubyId" => db.object_id,
        }
        $ICM_VARIABLES[db.object_id] = db
        response = {
            "Data" => data,
            "DataType" => "WSDatabase",
        }
    rescue => e
        response = error_response(e)
    end
    return response
end