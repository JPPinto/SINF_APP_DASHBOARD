$(document).ready(function() {
  // /api/Artigos
  $.getJSON("http://192.168.56.101/Primavera/api/Artigos", function(data) {
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
  $.getJSON("http://192.168.56.101/Primavera/api/Clientes", function(data) {
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
