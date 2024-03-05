import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Producto } from "src/interfaces/producto";

@Injectable()
export class ProductoProvider {
    
    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    Crear(producto: Producto): Observable<any> {
        const url = this.urlApi + 'api/Producto/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(producto);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerUltimo(): Observable<Producto> {
        const url = this.urlApi + 'api/Producto/ObtenerUltimo';
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Producto>(url, { 'headers': headers })
    }

    ObtenerTodos(): Observable<any> {
        const url = this.urlApi + 'api/Producto/ObtenerTodos';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    actualizarCostoProducto(): Observable<Producto> {
        const url = this.urlApi + 'api/Producto/ActualizarCosto';
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Producto>(url, { 'headers': headers })
    }

    ObtenerCantidad(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Producto/ObtenerCantidad');
    }
}