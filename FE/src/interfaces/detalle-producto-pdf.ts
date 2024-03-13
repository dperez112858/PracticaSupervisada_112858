export interface DetalleProductoPdf {
    listaDetalleProducto: DetalleProductoItem[];
    ok: boolean;
    statusCode: number;
    error: string | null;
}

export interface DetalleProductoItem {
    id: string;
    cantidad: number;
    precioInsumo: number;
    fechaCreacion: string;
    total: number;
    insumo: any | null; // Puedes ajustar este tipo según la interfaz de Insumo
    insumoId: string;
    producto: any | null; // Puedes ajustar este tipo según la interfaz de Producto
    productoId: string;
    ok: boolean;
    statusCode: number;
    error: string | null;
}
