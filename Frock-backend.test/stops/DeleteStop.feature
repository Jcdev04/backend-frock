Feature: Eliminar un paradero
    Como gestor de la empresa de transporte  
    Quiero poder eliminar un paradero  
    Para poder quitar las rutas que no se usan

Scenario: Eliminacion exitosa de un paradero
    Given no existe el paradero a eliminar
    When envio el id del paradero:
    | id |
    | 1  |
    Then el sistema elimina el paradero