require 'helper'

run_from_application do
  require 'CalculatorTestApp'
  include CalculatorTestApp
end

def define_python_methods
  @p.Execute("
def foo(x):
  return x + x

def bar(x):
  return x * x
"
  )
end

describe "Python Engine" do
  before do
    @p = PythonEngine.new
  end

  should 'create a engine' do
    @p.should.not.equal nil
    @p.Scope.should.equal nil
  end

  should 'execute a simple string of code' do
    @p.Execute("2 + 2").should.equal 4
  end

  should 'execute a function as a string' do
    @p.Execute("def foo(x):\n  return x + x\n\nfoo(5)").
      should.equal 10
  end

  should 'list defined methods' do
    only_ruby_methods = lambda do
      @p.ListOfMethods().reject{ |i| i.to_s =~ /__/ }
    end

    only_ruby_methods[].size.should == 0

    define_python_methods

    list = only_ruby_methods[]
    list.size.should == 2
    list.include?('foo'.to_clr_string).should.be.true
    list.include?('bar'.to_clr_string).should.be.true
  end

  should 'call a method' do
    define_python_methods

    @p.CallMethod('foo', 5).should.equal 10
    @p.CallMethod('bar', 5).should.equal 25
  end
end
