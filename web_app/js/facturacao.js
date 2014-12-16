$(document).ready(function(){

});


$('#submit').click(function(){
  console.log("Start: " + $('#start').val());
  console.log("End: " + $('#end').val());
});

$('#dia').click(function(){
  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    todayBtn:'true',
    autoclose: 'true',
    format: 'dd-mm-yyyy',
    minViewMode: 'days'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Dia');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});

$('#mes').click(function(){
  //$('.hero-unit').html($('#fake_from_container').html());
  //$('.input-daterange', $('.hero-unit')).attr('id', 'datepickerMonth');

  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    todayBtn:'true',
    autoclose: 'true',
    format: 'mm-yyyy',
    minViewMode: 'months'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Mês');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});

$('#ano').click(function(){
  $('#start').val('');
  $('#end').val('');

  $('#datepicker').datepicker("remove");
  $('#datepicker').datepicker({
    todayBtn:'true',
    autoclose: 'true',
    format: 'yyyy',
    minViewMode: 'years'
  });

  $('.input-daterange').css('display','');
  $('.dropdown-toggle').text('Ano');
  $('.dropdown-toggle').append("<span class=\"caret\"></span>");
});
