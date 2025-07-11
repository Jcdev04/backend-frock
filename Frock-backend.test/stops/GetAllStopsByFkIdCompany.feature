Feature: Obtener todos los paraderos por id de compañia
    Como gestor de la empresa de transporte  
    Quiero poder obtener todos los paraderos que maneja una compañia 

Scenario: Obtener paraderos por id de compañia
    Given no existe el paradero a obtener por id de compañia
    When envio el id de la compañia:
    | id |
    | 1  |
    Then el sistema muestra todos los parederos de la compañia