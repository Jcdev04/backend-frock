Feature: Modificar una compañia
    Como dueño de una compañia
    Quiero poder modificar los datos de mi compañia

Scenario: Modificacion exitosa de una compañia
    Given no existe la compañia a modificar
    When envio el id de la compañia con los datos a modificar:
    | id | name          | logoUrl                                                                                 | fkIdUser |
    | 1  | Test Companie | https://transportesostenible.com.pe/wp-content/uploads/2025/03/paradero-aerodirecto.jpg | 1        |
    Then el sistema actualiza la compañia
    And los campos de la compañia coinciden exactamente con los nuevos datos