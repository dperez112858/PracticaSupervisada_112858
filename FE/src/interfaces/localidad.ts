import { Provincia } from "./provincia";

export interface Localidad {
    id: string,
    nombre: string,
    Provincia: Provincia
}