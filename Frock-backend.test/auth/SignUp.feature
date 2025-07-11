Feature: Crear cuenta
  Como usuario quiero poder crear una cuenta
  Para poder usar los servicios

  Scenario: Creación exitosa de la cuenta
    Given no existe cuenta a crear
    When envío los datos para la nueva cuenta:
      | email          | username | password     | role |
      | user@email.com | user1    | password1234 | 1    |
    Then el sistema no devuelve error
    And el repositorio guarda la nueva cuenta
