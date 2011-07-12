<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
  <head>
    <title></title>
    <link href="/stylesheets/screen.css" media="all" rel="stylesheet" type="text/css"/>
    <script src="/libraries/LAB.src.js" type="text/javascript"></script>
  </head>
  <body>
    <script type="text/javascript">
        $LAB
        .script("/libraries/underscore.js").wait()
        .script("/libraries/knockout-1.2.1.debug.js").wait()
        .script("/libraries/json2.js").wait()
        .script("/libraries/jquery-1.6.2.js.js").wait()
        .script("/backbone.js").wait()
        .script("/controllers/account.js").wait()
        .script("/models/account/auth.js").wait()
        .script("/views/account.js").wait()
        .script("/app.js")
        .wait(function() {
            window.App = new AppView;
        });
    </script>
  </body>
</html>