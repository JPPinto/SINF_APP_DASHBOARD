<?php

$baseAPIURL = "http://192.168.108.202/Primavera/";
// Method: POST, PUT, GET etc
// Data: array("param" => "value") ==> index.php?param=value

function CallAPI($method, $url, $data = false)
{
    $curl = curl_init();

    switch ($method)
    {
        case "POST":
            curl_setopt($curl, CURLOPT_POST, 1);

            if ($data)
                curl_setopt($curl, CURLOPT_POSTFIELDS, http_build_query($data));
            break;
        case "PUT":
            curl_setopt($curl, CURLOPT_PUT, 1);
            break;
        default:
            if ($data)
                $url = sprintf("%s?%s", $url, http_build_query($data));
    }

    // Optional Authentication:
    //curl_setopt($curl, CURLOPT_HTTPAUTH, CURLAUTH_BASIC);
    //curl_setopt($curl, CURLOPT_USERPWD, "username:password");

    curl_setopt($curl, CURLOPT_URL, $url);
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);

    $output = curl_exec($curl);
    $info = curl_getinfo($curl);

    if ($info['http_code'] != 200 && $info['http_code'] != 201) {
      if(empty($output))
        $output = "No data returned for $url [". $info['http_code']. "]";
      else
        $output .= "\n[". $info['http_code']. "]";
      if (curl_error($curl))
        $output .= "\n". curl_error($curl);
    }
    else {
      if(empty($output))
        $output = "Success";
    }

    curl_close($curl);

    return $output;
}

function getFromAPI($url, $params = false)
{
  $result = CallAPI("GET", $url);
  return json_decode($result, true);
}

function postOnAPI($url, $params)
{
  $result = CallAPI("POST", $url, $params);
  echo $result;
}

function printArray($array, $title=null) {
  if($title!=null)
    echo "<h1>".$title."</h1>";
  //fill table header
  if(empty($array)) {
    echo "No contents";
  }
  else {
    echo "<table border=1><tr>";
    foreach($array[0] as $key => $value)
      echo "<th>".$key."</th>";
    echo "</tr>";

    foreach($array as $item) {
      echo "<tr>";
      foreach($item as $key=>$value) {
        echo "<td>";
        if(is_array($value))
          printArray($value);
        elseif($key=="Data")
          echo(date('d/m/Y', strtotime($value)));
        else
          echo $value;
        echo "</td>";
      }
      echo "</tr>";
    }
    echo "</table>";
  }
}
?>
