import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Producto } from "src/interfaces/producto";
import { PresupuestoU } from "src/interfaces/presupuestoU";
import { Presupuesto } from "src/interfaces/presupuesto";
import { IntervaloPresupuesto } from "src/interfaces/intervaloPresupuesto";
import { PresupuestoCompleto } from "src/interfaces/presupuesto-completo";

@Injectable()
export class PresupuestoProvider {
    
    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    Crear(presupuesto: Presupuesto): Observable<any> {
        const url = this.urlApi + 'api/Presupuesto/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(presupuesto);
        return this.http.post(url, body, { 'headers': headers });
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Presupuesto/Eliminar/?id=' + id, null);
    }
    // ObtenerUltimo(): Observable<Producto> {
    //     const url = this.urlApi + 'api/Producto/ObtenerUltimo';
    //     const headers = { 'content-type': 'application/json' }
    //     return this.http.get<Producto>(url, { 'headers': headers })
    // }

    ObtenerTodos(): Observable<any> {
        const url = this.urlApi + 'api/Presupuesto/ObtenerTodos';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    // actualizarCostoProducto(): Observable<Producto> {
    //     const url = this.urlApi + 'api/Producto/ActualizarCosto';
    //     const headers = { 'content-type': 'application/json' }
    //     return this.http.get<Producto>(url, { 'headers': headers })
    // }

    ObtenerCantidad(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Presupuesto/ObtenerCantidad');
    }

    ObtenerCantidadAceptados(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Presupuesto/ObtenerCantidadAceptados');
    }

    ObtenerPresupuestosPorMes(intervaloPresupuesto: IntervaloPresupuesto): Observable<any> {
        const url = this.urlApi + 'api/Presupuesto/ObtenerPresupuestosPorMes';
        const headers = { 'content-type': 'application/json' }
        const body = JSON.stringify(intervaloPresupuesto);
        return this.http.post(url, body, { 'headers': headers });
    }    

    ObtenerPresupuestosPorId(id: string): Observable<PresupuestoCompleto> {
        return this.http.get<PresupuestoCompleto>(this.urlApi + 'api/Presupuesto/ObtenerPdf?id=' + id);
    }
}