class Code
  DB_FILE = File.dirname(__FILE__) + "/../db/code.dat"
  DEFAULT = "def foo(x):\n  return x + x\n"
  
  def self.get
    data = nil
    if File.exist?(DB_FILE)
      File.open(DB_FILE, "rb"){|f| data = f.read}
      Marshal.load(data)
    else
      File.mkdir(File.dirname(DB_FILE)) unless File.directory?(File.dirname(DB_FILE))
      save(DEFAULT)
      DEFAULT
    end
  end

  def self.save(code)
    data = Marshal.dump(code)
    File.open(DB_FILE, "wb"){|f| f.write data}
  end
end
