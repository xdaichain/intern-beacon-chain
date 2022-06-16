
use rocksdb::DB;

fn main() {

    let mut db = DB::open_default("/Users/macbook/Desktop/nethermindLocalTestnet/private-networking/node_1/db").unwrap();
    let mut iter = db.raw_iterator();

    // Forwards iteration
    iter.seek_to_first();
    while iter.valid() {
        println!("Saw {:?} {:?}", iter.key(), iter.value());
        iter.next();
    }

    // Reverse iteration
    iter.seek_to_last();
    while iter.valid() {
        println!("Saw {:?} {:?}", iter.key(), iter.value());
        iter.prev();
    }

    // Seeking
    iter.seek(b"my key");
    while iter.valid() {
        println!("Saw {:?} {:?}", iter.key(), iter.value());
        iter.next();
    }

    // Reverse iteration from key
    // Note, use seek_for_prev when reversing because if this key doesn't exist,
    // this will make the iterator start from the previous key rather than the next.
    iter.seek_for_prev(b"my key");
    while iter.valid() {
        println!("Saw {:?} {:?}", iter.key(), iter.value());
        iter.prev();
    }
}