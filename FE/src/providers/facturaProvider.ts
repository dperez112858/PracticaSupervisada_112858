import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Factura } from "src/interfaces/factura";
import { Intervalo } from "src/interfaces/intervalo";

@Injectable()
export class FacturaProvider {
    
    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    Crear(factura: Factura): Observable<any> {
        const url = this.urlApi + 'api/Factura/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(factura);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerTodas(): Observable<any> {
        const url = this.urlApi + 'api/Factura/ObtenerTodas';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    ObtenerPedidosPorMes(intervalo: Intervalo): Observable<any> {
        const url = this.urlApi + 'api/Factura/ObtenerPedidosPorMes';
        const headers = { 'content-type': 'application/json' }
        const body = JSON.stringify(intervalo);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerPorCliente(id: any): Observable<any> {
        const url = this.urlApi + 'api/Factura/ObtenerPorCliente/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    ObtenerPorId(id?: any): Observable<Factura> {
        const url = this.urlApi + 'api/Factura/ObtenerPorId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Factura>(url, { 'headers': headers })
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Factura/Eliminar/?id=' + id, null);
    }

    Editar(factura: Factura): Observable<Factura> {
        const url = this.urlApi + 'api/Factura/Actualizar';
        return this.http.put<Factura>(url, factura);
    }

    ObtenerTotalAnioActual(): Observable<any> {
        const url = this.urlApi + 'api/Factura/ObtenerAnioActual';
        const headers = { 'content-type': 'application/json' }
        return this.http.get(url, { 'headers': headers })
    }

    ObtenerTotalAnioAnterior(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Factura/ObtenerAnioAnterior');
    }

    ObtenerActual(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Factura/ObtenerActual');
    }
    ObtenerAnterior(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Factura/ObtenerAnterior');
    }
}