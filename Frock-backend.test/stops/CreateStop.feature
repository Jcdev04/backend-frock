Feature: Creación de paradero  
    Como gestor de la empresa de transporte  
    Quiero crear un nuevo paradero  
    Para poder asociarlo después a rutas

Scenario: Creación exitosa de un paradero
    Given no existe un paradero llamado "Test Stop"
    When envío los datos del paradero:
  | name       | googleMapsUrl                        | imageUrl                      | phone        | fkIdCompany | address                           | reference         | fkIdDistrict |
  | Test Stop  | https://maps.google.com/test-stop    | https://transportesostenible.com.pe/wp-content/uploads/2025/03/paradero-aerodirecto.jpg | 123-456-7890 | 2           | 123 Test St, Test City, TC 12345 | Near the big tree | 1        |

    Then el sistema crea un nuevo paradero
    And el paradero tiene un Id numérico válido
    And los campos del paradero coinciden exactamente con los enviados  
