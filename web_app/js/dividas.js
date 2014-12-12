$(document).ready(function() {
  // /api/Artigos
  var nomes = [];
  var dados = [[],[]];
  $.getJSON(baseURL + "DividaCliente", function(data) {
      for (var i = 0; i < data.length; i++) {
        var linha = $('table#tableDivida>tbody').append('<tr><td>' + data[i].cliente + '</td><td>' + data[i].pendente + '</td><td>' + data[i].divida + '</tr>');
        nomes.push(data[i].cliente);
        dados[0].push(data[i].pendente);
        dados[1].push(data[i].divida);
      }
      var chartistData = {
        // A labels array that can contain any sort of values
        labels: nomes,
        // Our series array that contains series objects or in this case series data arrays
        series: dados
      };

      // Create a new line chart object where as first parameter we pass in a selector
      // that is resolving to our chart container element. The Second parameter
      // is the actual data object.
      new Chartist.Bar('.ct-chart', chartistData);
    })
    .fail(function() {
      console.log("error");
    });
});
