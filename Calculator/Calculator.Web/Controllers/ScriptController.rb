class ScriptController < Controller
  def save
    @@code = Request.Form["Code"]
    view nil, 'layout', @code
  end

  def index
    @@code = ScriptController.class_variable_defined?(:@@code) ?
      @@code : "def foo(x):\n  return x * x\n"
    view 'show', 'layout', @@code
  end
end
