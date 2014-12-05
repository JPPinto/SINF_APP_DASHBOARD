/*var request = $.ajax({
  type: "GET",
  url: "https://1abfe10c.ngrok.com/Primavera/api/Artigos",
  dataType: "jsonp"
});

request.done(function(data) {
  console.log("DATA: " + data);
});

request.always(function() {
  console.log("COMPLETE");
});

request.fail(function(err) {
  console.log("FALIED");
});*/


$(document).ready(function() {
  // /api/Artigos
  $.getJSON("https://1961ac36.ngrok.com/Primavera/api/Artigos", function(data) {
      console.log("DATA " + JSON.stringify(data));
      for (var i = 0; i < data.length; i++) {
        //{"CodArtigo":"FC.0002","DescArtigo":"Cravos"}
        $('table#tableArtigos').append('<tr><td>' + data[i].CodArtigo + '</td><td>' + data[i].DescArtigo + '</td></tr>');
      }
    })
    .fail(function() {
      console.log("error");
    });

  // /api/Clientes
  $.getJSON("https://1961ac36.ngrok.com/Primavera/api/Clientes", function(data) {
      console.log("DATA " + JSON.stringify(data));
      for (var i = 0; i < data.length; i++) {
        //{"CodCliente":"C0001","NomeCliente":"Restaurante ABC","NumContribuinte":"123456789","Moeda":"EUR","ModoPag":"NUM","CondPag":"1"}
        if(data[i].CodCliente == "VD")
          continue;
        $('table#tableClientes').append('<tr><td>' + data[i].CodCliente + '</td><td>' + data[i].NomeCliente + '</td><td>' + data[i].NumContribuinte + '</td></tr>');
      }
    })
    .fail(function() {
      console.log("error");
    });
});

/*

function createCORSRequest(method, url) {
  var xhr = new XMLHttpRequest();
  console.log(xhr);
  if ("withCredentials" in xhr) {

    // Check if the XMLHttpRequest object has a "withCredentials" property.
    // "withCredentials" only exists on XMLHTTPRequest2 objects.
    console.log("TEST");
    xhr.open(method, url, true);
    xhr.withCredentials = false;
    console.log("TEST2");
  } else if (typeof XDomainRequest != "undefined") {

    // Otherwise, check if XDomainRequest.
    // XDomainRequest only exists in IE, and is IE's way of making CORS requests.
    xhr = new XDomainRequest();
    xhr.open(method, url);

  } else {

    // Otherwise, CORS is not supported by the browser.
    xhr = null;

  }
  return xhr;
}

function makeCorsRequest() {
  // All HTML5 Rocks properties support CORS.
  var url = 'http://62dbb9e9.ngrok.com/Primavera/api/Clientes';

  var xhr = createCORSRequest('GET', url);
  if (!xhr) {
    alert('CORS not supported');
    return;
  }

  // Response handlers.
  xhr.onload = function() {
    var text = xhr.responseText;
    var title = getTitle(text);
    alert('Response from CORS request to ' + url + ': ' + title);
  };

  xhr.onerror = function() {
    alert('Woops, there was an error making the request.');
  };

  xhr.send();
  console.log(xhr.getResponseHeader());
}

makeCorsRequest();*/
