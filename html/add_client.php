<?php
  require_once('header.php');
  if(!empty($_POST))
  {
    require_once('globals.php');
    $data = json_encode($_POST);
    postOnAPI($baseAPIURL . "/api/clientes/", $_POST);
  }
  else {
?>
<h1>Novo Cliente</h1>
<form action="" method="post">
  <input type="text" name="CodCliente" placeholder="CodCliente">
  <input type="text" name="NomeCliente" placeholder="NomeCliente">
  <input type="text" name="NumContribuinte" placeholder="NumContribuinte">
  <input type="text" name="Moeda" placeholder="Moeda">
  <input type="Submit" value="OK">
</form>
<?php
  }
  require_once('footer.php');
?>
