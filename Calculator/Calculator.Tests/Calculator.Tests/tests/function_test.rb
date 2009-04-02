require 'helper'

describe 'Defining functions' do
  before do
    init_vars
    @functions.update_text ''
  end

  should 'show none with empty code' do
    @definitions.children.size.should.equal 0
  end

  should 'show none with incomplete code' do
    @functions.update_text @invalid
    @definitions.children.size.should.equal 0
  end

  should 'show with valid code' do
    @functions.update_text @valid
    @definitions.children.size.should.equal 2

    @definitions.children[0].tag.to_s.should == 'foo'
    @definitions.children[1].tag.to_s.should == 'baz'

    @definitions.children[0].content.to_s.should == 'foo(x)'
    @definitions.children[1].content.to_s.should == 'baz(x)'
  end

  should 'not retain buttons when moving from valid to invalid code' do
    @functions.update_text @valid
    @definitions.children.size.should.equal 2
    @functions.update_text @invalid
    @definitions.children.size.should.equal 0
  end
end

describe 'Running functions' do
  before do
    init_vars
    @functions.update_text "def foo(x):\n  return x + 2\n\ndef baz(x):\n  return x + 4\n\n"
    $calc.clear
  end

  should 'run a function' do
    $calc.do 9
    @page.RunCustomFunction(@definitions.children[0], nil)
    $calc.screen.should.equal '11'
    @page.RunCustomFunction(@definitions.children[1], nil)
    $calc.screen.should.equal '15'
  end
end
