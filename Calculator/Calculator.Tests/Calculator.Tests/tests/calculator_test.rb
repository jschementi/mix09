require 'helper'

describe 'Calculator' do
  after do
    $calc.clear
  end

  (0..9).each do |i|
    should "show \"#{i}\" on the screen" do
      $calc.click_button i
      $calc.screen.should.equal i.to_s
    end
  end

  should 'have matching current number and screen values' do
    $calc.current_number.to_string.to_s.
      should.equal $calc.screen
  end

  should 'add' do
    $calc.do 2, 'Add', 3, 'equals'
    $calc.screen.should.equal '5'
  end

  should 'subtract' do
    $calc.do 2, 'Subtract', 3, 'equals'
    $calc.screen.should.equal '-1'
  end

  should 'multiply' do
    $calc.do 2, 'Multiply', 3, 'equals'
    $calc.screen.should.equal '6'
  end

  should 'divide' do
    $calc.do 2, 'Divide', 4, 'equals'
    $calc.screen.should.equal '0.5'
  end

  should 'negate' do
    $calc.do 2, 'negate'
    $calc.screen.should.equal '-2'
  end

  should 'input a floating point number' do
    $calc.do 2, 'float', 3
    $calc.screen.should.equal '2.3'
  end

  describe 'Functions' do
    before do
      @functions = Application.current.root_visual.find_name('Functions')
      class << @functions
        def update_text(val)
          self.text = val
          Application.current.root_visual.Functions_TextChanged nil, nil
        end
      end
      @functions.update_text ''

      @definitions = Application.current.root_visual.find_name('FunctionDefinitions')

      @valid = "def foo(x):\n  return x + 2\n\ndef baz(x):\n  return x + 4\n\n"
      @invalid = "def foo(x)"
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
    end

    should 'not retain buttons when moving from valid to invalid code' do
      @functions.update_text @valid
      @definitions.children.size.should.equal 2
      @functions.update_text @invalid
      @definitions.children.size.should.equal 0
    end
  end
end
