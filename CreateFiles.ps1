$execFile = "C:\Users\yassine.saioud-prest\source\repos\TemplateBuilder\TemplateBuilder.Console\bin\Debug\TemplateBuilder.Console.exe"
$params = "-create","C:\Users\yassine.saioud-prest\source\repos\TemplateBuilder\TemplateBuilder.Console\bin\Debug\Template.json"

 # Wait until the started process has finished
 & $execFile $params
 if (-not $?)
 {
    # Show error message
 }

