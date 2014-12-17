//var chartFacturasLabels = [];
//var chartFacturasSeries = [];

function dateFromDay(year, day){
  var date = new Date(year, 0); // initialize a date in `year-01-01`
  var temp =  new Date(date.setDate(day)); // add the number of days
  return (temp.getMonth() + 1) + '/' + temp.getDate();
}

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
    labelOffset: 125,
    chartPadding: 50
  }]
];

$('#submit').click(function(){
  console.log("Start: " + $('#start').val());
  console.log("End: " + $('#end').val());

  if($('#start').val() == "" || $('#end').val() == ""){
    alert("Ambos os campos são necessários à definição do periodo de facturacao.");
    return;
  }

  if($('#start').val() > $('#end').val() ){
    alert("A data incial tem de ser posterior à data final.");
    return;
  }

  var startDate = $('#start').val();
  var endDate = $('#end').val();
  var dateType = $('.dropdown-toggle').text().toLowerCase();

  if(dateType === "ano"){
    //TABLE NAME PARTE EDIT TODO
    dateType = "year";
    startDate += "-01-01T00:00:00";
    endDate += "-01-01T00:00:00"
  }

  if(dateType === "mes"){
    dateType = "month";
    startDate = startDate + "-01T00:00:00";
    endDate = endDate + "-01T00:00:00"
  }

  if(dateType === "dia"){
    dateType = "dayofyear";
  }

  $.getJSON(baseURL + "Faturacao", {'dateBegin':startDate, 'dateEnd':endDate,'datePart':dateType}, function(data) {
    console.log("DATA: " + JSON.stringify(data));

    $('table#tableFacturacao>tbody').empty();

    for (var i = 0; i < data.length; i++) {
      //chartFacturaLabels.push(data[i].ano);
      //chartFacturaSeries.push(data[i].total);

      var appendAno = '<td>' + data[i].ano + '</td>';
      var appendParte = '<td>' + data[i].parte + '</td>';
      var appendTotal = '<td>' + data[i].total + '</td>';

      if(dateType === "dayofyear"){
        appendParte = '<td>' + dateFromDay(data[i].ano,data[i].parte) + '</td>';
        $('thead>tr>th.second').css('display','');
        $('thead>tr>th.second').text("MM/DD");
      }

      if(dateType === "month"){
        $('thead>tr>th.second').css('display','');
        $('thead>tr>th.second').text("MM");
      }

      if(dateType === "year"){
        appendParte = '';
        $('thead>tr>th.second').css('display','none');
      }

      $('table#tableFacturacao>tbody').append('<tr>' + appendAno + appendParte + appendTotal + '</tr>');
    }

    $('div#table').css('display','');

    if(data.length <= 0) {
      $('table#tableFacturacao>tbody').append('<tr><td>NO_RESULTS</td><td>NO_RESULTS</td><td>NO_RESULTS</td></tr>');
    }


  });
});

$('#dia').click(function(){
  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    orientation:'top',
    todayBtn:'true',
    autoclose: 'true',
    format: 'yyyy-mm-dd',
    minViewMode: 'days'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Dia');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});

$('#mes').click(function(){
  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    orientation:'top',
    todayBtn:'true',
    autoclose: 'true',
    format: 'yyyy-mm',
    minViewMode: 'months'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Mes');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});

$('#ano').click(function(){
  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    orientation:'top',
    todayBtn:'true',
    autoclose: 'true',
    format: 'yyyy',
    minViewMode: 'years'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Ano');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});
