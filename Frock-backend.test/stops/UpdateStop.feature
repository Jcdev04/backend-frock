Feature: Modificar un paradero
    Como gestor de la empresa de transporte  
    Quiero poder modificar un paradero  
    Para poder actualizar la ruta

Scenario: Modificacion exitosa de un paradero
    Given no existe el paradero a modificar
    When envio el id del paradero con los datos a modificar:
    | id | name      | googleMapsUrl                     | imageUrl                                                                                | phone        | fkIdCompany | address                          | reference         | fkIdDistrict |
    | 1  | Test Stop | https://maps.google.com/test-stop | https://transportesostenible.com.pe/wp-content/uploads/2025/03/paradero-aerodirecto.jpg | 123-456-7890 | 2           | 123 Test St, Test City, TC 12345 | Near the big tree | 1            |
    Then el sistema actualiza el paradero
    And los campos del paradero coinciden exactamente con los nuevos datos