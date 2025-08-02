def error_response(e)
  data = {'Message' => e.message}
  response = {
    "Data" => data,
    "DataType" => "WSError"
  }
  return response
end