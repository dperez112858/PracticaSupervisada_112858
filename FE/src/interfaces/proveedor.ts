import { CondicionIva } from "./condicionIva";
import { Localidad } from "./localidad";

export interface Proveedor{
    id: string,
    razonSocial: string,
    cuit: string,
    calle: string,
    numero: string,
    codigoPostal: string,
    telefono: string,
    nombreContacto: string,
    email: string,
    comentario: string,
    localidad: Localidad,
    condicionIva: CondicionIva
}