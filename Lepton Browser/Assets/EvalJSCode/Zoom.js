function ZoomFunction(Percentage) {
  document.body.style.zoom = Percentage + "%";
}

function GetZoomLevelIE7() {
  var level = 100;
  if (document.body.getBoundingClientRect) {
    // rect is only in physical pixel size in IE before version 8 
    var rect = document.body.getBoundingClientRect();
    var physicalW = rect.right - rect.left;
    var logicalW = document.body.offsetWidth;

    // the zoom level is always an integer percent value
    level = Math.round((physicalW / logicalW) * 100);
  }
  return level;
}

function GetZoomLevelIE() {
  // IE before version 8
  if (screen.systemXDPI === undefined) {
    return GetZoomLevelIE7();
  }
  // the zoom level is always an integer percent value
  return Math.round((screen.deviceXDPI / screen.logicalXDPI) * 100);
}

function GetMagnification() {
  var message = "";
  if ('deviceXDPI' in screen) {
    var zoomLevel = GetZoomLevelIE();
    message += "The current zoom level is " + zoomLevel + "%.";
  }
  else {  // Firefox, Opera, Google Chrome and Safari
    message = "Your browser does not support this example!";
  }

  var info = document.getElementById("info");
  info.innerHTML = message;
}

var RegisterZoomMonitor = new function () {
  var self = this;

  self.TryRegisterZoomMonitor = function () {
    window.ZoomMonitor.log("xxxx");
    window.addEventListener(
      'resize',
      function (event) {
        if ('deviceXDPI' in screen) {
          var zoomLevel = GetZoomLevelIE();
          if (window.ZoomMonitor)
            window.ZoomMonitor.didZoomLevelChanged(zoomLevel);
        }
      });
  }
}