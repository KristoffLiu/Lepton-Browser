﻿var webViewId = "";
var targetOnContextMenu = null;
document.oncontextmenu = function (event) {
  console.debug('content menu');
  var nodeName = null;
  var nodeType = null;
  if (event.target.tagName != null && event.target.tagName.length > 0)
    nodeName = event.target.tagName.toLowerCase();
  if (event.target.type != null && event.target.type.length > 0)
    nodeType = event.target.type.toLowerCase();
  // If the node is input element, return true to prevent to show our context menu.
  // Then, the original context menu will be shown.
  if (nodeName === 'input' && (nodeType === 'text' || nodeType === 'password'))
    return true;

  targetOnContextMenu = event.target;
  var subType = event.target.type;
  console.log(subType);
  var href = findLinkUrl(event.target, 3);
  var image = findImage(event.target);
  var selection = hasSelection();
  if (window.WebViewBridge) {
    window.WebViewBridge.showContextMenu(webViewId, event.clientX, event.clientY,
      nodeName, subType, href, image, selection);
    return false;
  }
  return true;
}

function findImage(node) {
  if (node.nodeName.toLowerCase() === 'img') {
    return node.src;
  }
  return '';
}

function findLinkUrl(node, levels) {
  var count = 0;
  while (node) {
    if (node.nodeName.toLowerCase() === 'a' && node.href != null) {
      node.blur();
      return node.href;
    }
    if (++count > levels)
      break;
    node = node.parentNode;
  }
  return '';
}

function hasSelection() {
  return getSelectionText().length > 0;
}

function getSelectionText() {
  return window.getSelection().toString();
}

function setWebViewId(id) {
  webViewId = id;
  return webViewId;
}

function execCommand(command) {
  targetOnContextMenu.focus();
  var successful = document.execCommand(command);
  return successful ? 'successful' : 'unsuccessful';
}

function scrollPageUp() {
  scrollBy(0, -window.innerHeight);
  return "value-" + window.innerHeight;
}

function scrollPageDown() {
  scrollBy(0, window.innerHeight);
  return "value+" + window.innerHeight;
}

function scrollPageTop() {
  scrollTo(0, 0);
  return "value+0";
}

function scrollPageBottom() {
  scrollTo(0, document.body.scrollHeight);
  return "value+" + document.body.scrollHeight;
}