require 'rubygems'
require 'net/ssh'
require 'net/scp'

Net::SCP.start(
  "jimmy.schementi.com", 
  "jschementi", {
  :password => File.open("#{File.dirname(__FILE__)}/pswd") do |f| 
                 f.read
               end.chomp
}) do |scp|
  ["IronRuby", "IronPython", "Microsoft.Scripting"].each do |file|
    filename = File.expand_path(File.dirname(__FILE__)) + "/bin/#{file}.slvx"
    print "Uploading #{file}..."
    scp.upload! filename, "/home/jschementi/jimmy.schementi.com/jflamdemo/#{filename.split("/").last}"
    puts "done"
  end
end
