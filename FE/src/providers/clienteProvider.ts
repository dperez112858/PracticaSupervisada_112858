import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { Cliente } from "../interfaces/cliente";

@Injectable()
export class ClienteProvider {

    urlApi: string = environment.urlApi
    cliente = {} as Cliente;

    constructor(private http: HttpClient) {
    }

    Crear(cliente: Cliente): Observable<any> {
        const url = this.urlApi + 'api/Cliente/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(cliente);
        return this.http.post(url, body, { 'headers': headers });
    }

    // ObtenerTodos(): Observable<any> {
    //     const url = this.urlApi + 'api/Cliente/ObtenerTodos';
    //     const headers = { 'content-type': 'application/json' }
    //     return this.http.get(url, { 'headers': headers })
    // }

    ObtenerTodos(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Cliente/ObtenerTodos');
    }

    // Editar(cliente: Cliente): Observable<Cliente> {
    //     return this.http.put<Cliente>(this.urlApi + cliente.Id, cliente);
    // }
    Editar(cliente: Cliente): Observable<any> {
        const url = this.urlApi + 'api/Cliente/Actualizar/';
        return this.http.put<Cliente>(url, cliente);
    }


    // obtenerCliente(id?: string): Observable<Cliente> {
    //     const url = this.urlApi + 'api/Cliente/ObtenerPorId/' + id;
    //     const headers = { 'content-type': 'application/json' }
    //     return this.http.get<Cliente>(url, { 'headers': headers })
    // }

    obtenerCliente(id: string): Observable<Cliente> {
        return this.http.get<Cliente>(this.urlApi + 'api/Cliente/ObtenerPorId/?id=' + id);
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Cliente/Eliminar/?id=' + id, null);
    }

}