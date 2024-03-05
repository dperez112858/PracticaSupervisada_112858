import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { Proveedor } from "../interfaces/proveedor";

@Injectable()
export class ProveedorProvider {

    urlApi: string = environment.urlApi
    proveedor = {} as Proveedor;

    constructor(private http: HttpClient) {
    }

    Crear(proveedor: Proveedor): Observable<any> {
        const url = this.urlApi + 'api/Proveedor/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(proveedor);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerTodos(): Observable<any> {
        const url = this.urlApi + 'api/Proveedor/ObtenerTodos';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    Editar(proveedor: Proveedor): Observable<any> {
        const url = this.urlApi + "api/Proveedor/Actualizar/";
        return this.http.put<Proveedor>(url, proveedor);
    }

    obtenerProveedor(id?: string): Observable<Proveedor> {
        const url = this.urlApi + 'api/Proveedor/ObtenerPorId/' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Proveedor>(url, { 'headers': headers })
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Proveedor/Eliminar/?id=' + id, null);
    }

    ObtenerPorId(id?: any): Observable<Proveedor> {
        const url = this.urlApi + 'api/Proveedor/ObtenerPorId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Proveedor>(url, { 'headers': headers })
    }
}