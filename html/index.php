<?php
  require_once('header.php');
  require_once('globals.php');

  try {
    $artigos = getFromAPI($baseAPIURL . "/api/artigos/");
    printArray($artigos, "Artigos");

    $clientes = getFromAPI($baseAPIURL . "/api/clientes/");
    printArray($clientes, "Clientes");
    echo "<p><a href='add_client.php'>Adicionar cliente</a></p>";

    $docsvenda = getFromAPI($baseAPIURL . "/api/docvenda/");
    printArray($docsvenda, "Docs Venda");
    echo "<p><a href='add_saledoc.php'>Adicionar documento de venda</a></p>";

    $docscompra = getFromAPI($baseAPIURL . "/api/doccompra/");
    printArray($docscompra, "Docs Compra");
    echo "<p><a href='add_purchasedoc.php'>Adicionar documento de compra</a></p>";
  }
  catch (Exception $e) {
    echo 'EXCEPTION: ',$e->getMessage();
  }
  require_once('footer.php');
?>
