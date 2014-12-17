$(document).ready(function() {
  var chartLabels = [];
  var chartSeries = [];
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
  $.getJSON(baseURL + "FaturacaoFamilia", function(data) {
      for (var i = 0; i < data.length; i++) {
        chartLabels.push(data[i].codFamilia);
        chartSeries.push(data[i].total);
        $('table#tableFacFamilia>tbody').append('<tr><td>' + data[i].codFamilia + '</td><td>' + data[i].descricao + '</td><td>' + data[i].total + '</td></tr>');
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
      new Chartist.Pie('.ct-chart', chartistData, null, responsiveOptions);
    })
    .fail(function() {
      console.log("error");
    });
});
