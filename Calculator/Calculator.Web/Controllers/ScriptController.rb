class ScriptController < Controller
  def save
    @@code = Request.Form["Code"]
    view nil, 'layout', @code
  end

  def index 
    @codeview = ScriptController.class_variable_defined?(:@@code) ?
      @@code : 'No code yet!'
    view 'show', 'layout', @codeview
  end
end
