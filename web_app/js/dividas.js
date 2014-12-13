$(document).ready(function() {
  // /api/Artigos
  var chartLabels = [];
  var chartSeries = [[],[]];
  $.getJSON(baseURL + "DividaCliente", function(data) {
      for (var i = 0; i < data.length; i++) {
        chartLabels.push(data[i].codcliente);
        chartSeries[0].push(data[i].pendente);
        chartSeries[1].push(data[i].divida);
        $('table#tableDivida>tbody').append('<tr><td>' + data[i].codcliente + '</td><td>' + data[i].nomecliente + '</td><td>' + data[i].pendente + '</td><td>' + data[i].divida + '</tr>');
      }
      var chartistData = {
        // A labels array that can contain any sort of values
        labels: chartLabels,
        // Our series array that contains series objects or in this case series data arrays
        series: chartSeries
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
