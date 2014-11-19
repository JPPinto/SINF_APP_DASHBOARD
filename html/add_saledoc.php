<?php
require_once('header.php');
require_once('globals.php');
if(!empty($_POST))
{
  $data = json_encode($_POST);
  postOnAPI($baseAPIURL . "/api/DocVenda/", $_POST);
}
else {
?>
<h1>Novo Documento de Venda</h1>
<form action="" method="post">
  <select name="Entidade" id="Entidade">
<?php
  $clientes = getFromAPI($baseAPIURL . "/api/clientes/");
  foreach($clientes as $cliente)
    echo '<option value="'.$cliente['CodCliente'].'" data-condpag="'.
    $cliente['CondPag'].'" data-modopag="'.$cliente['ModoPag'].'">'.
    $cliente['NomeCliente'].'</option>';
?>
  </select>
  <select name="CondPag" id="CondPag">
<?php
  $condspag = getFromAPI($baseAPIURL . "/api/condpag/");
  foreach($condspag as $condpag)
    echo '<option value="'.$condpag['Codigo'].'">'.$condpag['Descricao'].'</option>';
?>
  </select>
  <select name="ModoPag" id="ModoPag">
<?php
  $modospag = getFromAPI($baseAPIURL . "/api/modopag/");
  foreach($modospag as $modopag)
    echo '<option value="'.$modopag['Codigo'].'">'.$modopag['Descricao'].'</option>';
?>
  </select>
  <br /><br />
  <div>
    <table border=1>
      <tr>
        <th>CodArtigo</th>
        <th>Quantidade</th>
        <th>PrecoUnitario</th>
        <th>Desconto</th>
      </tr>
      <tr>
        <td>
          <select name="LinhasDoc[0][CodArtigo]">
            <?php
              $artigos = getFromAPI($baseAPIURL . "/api/artigos/");
              foreach($artigos as $artigo)
                echo '<option value="'.$artigo['CodArtigo'].'">'.$artigo['DescArtigo'].'</option>';
            ?>
          </select>
        </td>
        <td>
          <input type="text" name="LinhasDoc[0][Quantidade]">
        </td>
        <td>
          <input type="text" name="LinhasDoc[0][PrecoUnitario]">
        </td>
        <td>
          <input type="text" name="LinhasDoc[0][Desconto]">
        </td>
      </tr>
    </table>
  </div>
  <br />
  <input type="Submit" value="OK">
</form>
<script>
  $( "#Entidade" ).on( "change", function() {
    $("#CondPag").val($(this).find(":selected").attr("data-condpag"));
    $("#ModoPag").val($(this).find(":selected").attr("data-modopag"));
  }).change();
</script>
<?php
  }
  require_once('footer.php');
?>
