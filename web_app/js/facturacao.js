function dateFromDay(year, day){
  var date = new Date(year, 0); // initialize a date in `year-01-01`
  var temp =  new Date(date.setDate(day)); // add the number of days
  return (temp.getMonth() + 1) + '/' + temp.getDate();
}

$('button#submit').click(function(){
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
  var dateEND = new Date($('#end').data('datepicker').getFormattedDate('yyyy-mm-dd'));
  console.log(dateEND);
  var startDate = $('#start').val();
  var endDate = $('#end').val();
  var dateType = $('#menuPeriodo').text().toLowerCase();

  if(dateType === "ano"){
    dateType = "year";
    dateEND.setMonth(11);
    startDate += "-01-01";
    endDate = dateEND.getFullYear() + "-" + (dateEND.getMonth() + 1) + "-" + dateEND.getDate();
  }

  if(dateType === "mes"){
    dateType = "month";
    dateEND.setMonth(dateEND.getMonth() + 1);
    startDate = startDate + "-01";
    endDate = dateEND.getFullYear() + "-" + (dateEND.getMonth() + 1) + "-" + dateEND.getDate();
  }

  if(dateType === "dia"){
    dateType = "dayofyear";
  }

  $.getJSON(baseURL + "Faturacao", {'dateBegin':startDate, 'dateEnd':endDate,'datePart':dateType}, function(data) {
    var chartFacturasLabels = [];
    var chartFacturasSeries = [[]];
    console.log("DATA: " + JSON.stringify(data));

    $('table#tableFacturacao>tbody').empty();


    for (var i = 0; i < data.length; i++) {

      chartFacturasSeries[0].push(data[i].total);

      var appendAno = '<td>' + data[i].ano + '</td>';
      var appendParte = '<td>' + data[i].parte + '</td>';
      var appendTotal = '<td>' + data[i].total + '</td>';

      if(dateType === "dayofyear"){
        var date = dateFromDay(data[i].ano,data[i].parte);
        chartFacturasLabels.push(date);
        appendParte = '<td>' + date + '</td>';
        $('thead>tr>th.second').css('display','');
        $('thead>tr>th.second').text("MM/DD");
      }

      if(dateType === "month"){
        chartFacturasLabels.push(data[i].parte);
        $('thead>tr>th.second').css('display','');
        $('thead>tr>th.second').text("MM");
      }

      if(dateType === "year"){
        chartFacturasLabels.push(data[i].parte);
        appendParte = '';
        $('thead>tr>th.second').css('display','none');
      }

      $('table#tableFacturacao>tbody').append('<tr>' + appendAno + appendParte + appendTotal + '</tr>');
    }

    $('div#table').css('display','');

    if(data.length <= 0) {
      $('table#tableFacturacao>tbody').append('<tr><td>NO_RESULTS</td><td>NO_RESULTS</td><td>NO_RESULTS</td></tr>');
      return;
    }

    var chartistData = {
      // A labels array that can contain any sort of values
      labels: chartFacturasLabels,
      // Our series array that contains series objects or in this case series data arrays
      series: chartFacturasSeries
    };

    new Chartist.Line('.ct-chart#chartFacturacao', chartistData, {
                                                                  showPoint:false,
                                                                  lineSmooth:false,
                                                                  showArea: true
                                                                });
  })
  .fail(function() {
    console.log("error");
  });
});

$('button#submitTrimestre').click(function(){

  var ano = $('#anoTrimestre').val();
  var trimestre = $('#menuPicker').text();
  console.log(ano + '    ' + trimestre);
  if(ano === "" || trimestre === 'Trimestre de Facturação'){
    alert("É necessário a selecção de um trimestre e um ano.");
    return;
  }

  var startDate;
  var endDate;
  var dateType = "month";

  switch (trimestre){
    case "1º Trimestre (Jan, Fev, Mar, Abr)":
      startDate = ano + '-01-01';
      endDate = ano + '-04-30';
      break;
    case "2º Trimestre (Mai, Jun, Jul, Ago)":
      startDate = ano + '-05-01';
      endDate = ano + '-08-31';
      break;
    case "3º Trimestre (Set, Out, Nov, Dez)":
      startDate = ano + '-09-01';
      endDate = ano + '-12-31';
      break;
    default:
      startDate = ano + '-01-01';
      endDate = ano + '-01-01';
      break;
  }

  $.getJSON(baseURL + "Faturacao", {'dateBegin':startDate, 'dateEnd':endDate,'datePart':dateType}, function(data) {
    var chartFacturasLabels = [];
    var chartFacturasSeries = [[]];
    console.log("DATA: " + JSON.stringify(data));

    $('table#tableFacturacao>tbody').empty();

    for (var i = 0; i < data.length; i++) {

      chartFacturasSeries[0].push(data[i].total);

      var appendAno = '<td>' + data[i].ano + '</td>';
      var appendParte = '<td>' + data[i].parte + '</td>';
      var appendTotal = '<td>' + data[i].total + '</td>';

      chartFacturasLabels.push(data[i].parte);
      $('thead>tr>th.second').css('display','');
      $('thead>tr>th.second').text("MM");
      $('table#tableFacturacao>tbody').append('<tr>' + appendAno + appendParte + appendTotal + '</tr>');
    }

    $('div#table').css('display','');

    if(data.length <= 0) {
      $('table#tableFacturacao>tbody').append('<tr><td>NO_RESULTS</td><td>NO_RESULTS</td><td>NO_RESULTS</td></tr>');
      $('#chartFacturacao').css('display','none');
      return;
    }

    var chartistData = {
      // A labels array that can contain any sort of values
      labels: chartFacturasLabels,
      // Our series array that contains series objects or in this case series data arrays
      series: chartFacturasSeries
    };

    new Chartist.Line('.ct-chart#chartFacturacao', chartistData, {
                                                                  showPoint:false,
                                                                  lineSmooth:false,
                                                                  showArea: true
                                                                });
  })
  .fail(function() {
    console.log("error");
  });
});

$('#primeiroTrimestre').click(function(){
  $('#menuPicker').text('1º Trimestre (Jan, Fev, Mar, Abr)');
  $('#menuPicker').append("<span class=\"caret\"></span>");
});

$('#segundoTrimestre').click(function(){
  $('#menuPicker').text('2º Trimestre (Mai, Jun, Jul, Ago)');
  $('#menuPicker').append("<span class=\"caret\"></span>");
});

$('#terceiroTrimestre').click(function(){
  $('#menuPicker').text('3º Trimestre (Set, Out, Nov, Dez)');
  $('#menuPicker').append("<span class=\"caret\"></span>");
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
    minViewMode: 'days',
    language: 'pt'
  });

  $('#trimesterPicker').css('display','none');
  $('.input-daterange').css('display','');
  $('#menuPeriodo').text('Dia');
  $('#menuPeriodo').append("<span class=\"caret\"></span>");
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
    minViewMode: 'months',
    language: 'pt'
  });

  $('#trimesterPicker').css('display','none');
  $('.input-daterange').css('display','');
  $('#menuPeriodo').text('Mes');
  $('#menuPeriodo').append("<span class=\"caret\"></span>");
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
    minViewMode: 'years',
    language: 'pt'
  });

  $('#trimesterPicker').css('display','none');
  $('.input-daterange').css('display','');
  $('#menuPeriodo').text('Ano');
  $('#menuPeriodo').append("<span class=\"caret\"></span>");
});

$('#trimestre').click(function(){

  $('#anoTrimestre').text("");
  $('#anoTrimestre').datepicker({
    orientation:'top',
    autoclose: 'true',
    format: 'yyyy',
    minViewMode: 'years',
    language: 'pt'
  });

  $('.input-daterange').css('display','none');
  $('#trimesterPicker').css('display','');
  $('#menuPeriodo').text('Trimestre');
  $('#menuPeriodo').append("<span class=\"caret\"></span>");
  $('#menuPicker').text('Trimestre de Facturação');
  $('#menuPicker').append("<span class=\"caret\"></span>");
});
