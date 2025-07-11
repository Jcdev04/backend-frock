Feature: Crear cuenta
    Como usuario quiero poder crear una cuenta
    Quiero crear una cuenta
    Para poder usar los servicios

Scenario: Creacion exitosa de la cuenta
    Given no existe cuenta a crear
    When envío los datos para la nueva cuenta:
    | email          | userName | password     | role |
    | user@email.com | user1    | password1234 | 1    |
    Then el sistema crea una nueva cuenta
    And la cuenta tiene un Id numérico válido
    And el nombre y correo de la cuenta creada coincide con lo ingresado