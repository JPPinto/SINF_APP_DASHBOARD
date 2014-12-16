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
    dateType = "day";
  }

  $.getJSON(baseURL + "Faturacao", {'dateBegin':startDate, 'dateEnd':endDate,'datePart':dateType}, function(data) {
    console.log("DATA: " + JSON.stringify(data));

    $('table#tableFacturacao>tbody').empty();

    for (var i = 0; i < data.length; i++) {
      //chartLabels.push(data[i].codcliente);
      //chartSeries[0].push(data[i].pendente);
      //chartSeries[1].push(data[i].divida);
      $('table#tableFacturacao>tbody').append('<tr><td>' + data[i].ano + '</td><td>' + data[i].parte + '</td><td>' + data[i].total + '</td></tr>');
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
