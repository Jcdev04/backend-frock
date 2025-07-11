Feature: Creación de compañia  
    Como dueño de una empresa de transporte  
    Quiero crear mi compañia

Scenario: Creación exitosa de una comañia
    Given no existe la compañia a crear
    When envío los datos de la compañia:
    | name          | logoUrl                           | fkIdUser |
    | Test Companie | https://maps.google.com/test-stop | 1        |
    Then el sistema crea la compañia
    And la compañia tiene un Id numérico válido
    And los campos de la compañia coinciden exactamente con los enviados