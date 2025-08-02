#Require all files in the database folder
file_names = Dir.glob(File.join(File.dirname(__FILE__), '*.rb'))


puts file_names
file_names.each do |file|
  require file
end