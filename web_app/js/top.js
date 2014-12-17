$("input:button#submit").click(function() {
  var numLinhas = $("input#numLinhas").val();
  if(numLinhas>0) {
    // /api/Artigos
    var chartVendasLabels = [];
    var chartVendasSeries = [];
    var chartComprasLabels = [];
    var chartComprasSeries = [];

    var responsiveOptions = [
      ['screen and (min-width: 640px)', {
        chartPadding: 30,
        labelOffset: 100,
        labelDirection: 'explode',
        labelInterpolationFnc: function(value) {
          return value;
        }
      }],
      ['screen and (min-width: 1024px)', {
        labelOffset: 150,
        chartPadding: 50
      }]
    ];

    $.getJSON(baseURL + "TopVenda", {'numLinhas': numLinhas}, function(data) {
        $('table#tableTopVendas>tbody').html("");
        for (var i = 0; i < data.length; i++) {
          chartVendasLabels.push(data[i].NomeArtigo);
          chartVendasSeries.push(data[i].Quantidade);
          $('table#tableTopVendas>tbody').append('<tr><td>' + data[i].CodArtigo + '</td><td>' + data[i].NomeArtigo + '</td><td>' + data[i].Quantidade + '</td></tr>');
        }
        var chartistData = {
          // A labels array that can contain any sort of values
          labels: chartVendasLabels,
          // Our series array that contains series objects or in this case series data arrays
          series: chartVendasSeries
        };

        // Create a new line chart object where as first parameter we pass in a selector
        // that is resolving to our chart container element. The Second parameter
        // is the actual data object.
        new Chartist.Pie('.ct-chart#chartTopVendas', chartistData, null, responsiveOptions);
      })
      .fail(function() {
        console.log("error");
      });

    $.getJSON(baseURL + "TopCompra", {'numLinhas': numLinhas}, function(data) {
        $('table#tableTopCompras>tbody').html("");
        for (var i = 0; i < data.length; i++) {
          chartComprasLabels.push(data[i].NomeArtigo);
          chartComprasSeries.push(data[i].Quantidade);
          $('table#tableTopCompras>tbody').append('<tr><td>' + data[i].CodArtigo + '</td><td>' + data[i].NomeArtigo + '</td><td>' + data[i].Quantidade + '</td></tr>');
        }
        var chartistData = {
          // A labels array that can contain any sort of values
          labels: chartComprasLabels,
          // Our series array that contains series objects or in this case series data arrays
          series: chartComprasSeries
        };

        // Create a new line chart object where as first parameter we pass in a selector
        // that is resolving to our chart container element. The Second parameter
        // is the actual data object.
        new Chartist.Pie('.ct-chart#chartTopCompras', chartistData, null, responsiveOptions);
      })
      .fail(function() {
        console.log("error");
      });
    $(".row.hide").removeClass("hide");
  }
});
