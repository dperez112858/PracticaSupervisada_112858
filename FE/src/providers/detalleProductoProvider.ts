import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { DetalleProducto } from "src/interfaces/detalleProducto";

@Injectable()
export class DetalleProductoProvider {
    
    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    Crear(detalleProducto: DetalleProducto): Observable<any> {
        const url = this.urlApi + 'api/DetalleProducto/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(detalleProducto);
        return this.http.post(url, body, { 'headers': headers });
    }

    ActualizarProducto(detalleProducto: DetalleProducto): Observable<any> {
        const url = this.urlApi + "api/DetalleProducto/Actualizar/";
        return this.http.put<DetalleProducto>(url + detalleProducto.Id, detalleProducto);
    }

    ObtenerTodosPorProductoId(id?: any): Observable<DetalleProducto[]> {
        const url = this.urlApi + 'api/DetalleProducto/ObtenerTodosPorProductoId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<DetalleProducto[]>(url, { 'headers': headers })
    }

    ObtenerPorId(id?: any): Observable<DetalleProducto> {
        const url = this.urlApi + 'api/DetalleProducto/ObtenerPorId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<DetalleProducto>(url, { 'headers': headers })
    }

    ObtenerTodos(): Observable<any> {
        const url = this.urlApi + 'api/DetalleProducto/ObtenerTodos';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    ObtenerTodosUltimoProducto(): Observable<any> {
        const url = this.urlApi + 'api/DetalleProducto/ObtenerTodosUltimoProducto';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }
}