var RegisterDragDropHandler = new function () {
  var self = this;

  self.TryRegisterDragDropHandler = function () {
    document.body.addEventListener(
      'dragover',
      function (event) {
        console.debug('ondragover');
        if (window.superDragDropClient.allowDrag() == true)
          event.preventDefault();
      });

    document.body.addEventListener(
      'dragstart',
      function (event) {
        var data = event.srcElement.innerHTML;
        if (window.superDragDropClient)
          window.superDragDropClient.didDragStart(data);
      });

    document.body.addEventListener(
      'drop',
      function (event) {
        var data = event.dataTransfer.getData("Text");
        if (window.superDragDropClient)
          window.superDragDropClient.didDrop(data);
      });
  }
}