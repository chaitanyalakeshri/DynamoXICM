require 'json'
def evaluate_response(response)
  response = JSON.parse(response)
  return send(response["function"], *response["args"])
end

# #Test
# def test_func(param1,param2,param3)
#   puts "Param1: #{param1}"
#   puts "Param2: #{param2}"
#   puts "Param3: #{param3}"
#   return "Success"
# end
# json={
#   "function": "test_func",
#   "params": ["1","2","3"],
# }
# puts evaluate_response(json.to_json)